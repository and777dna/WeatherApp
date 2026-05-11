using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services.Implementations;

public class WeatherService(ICityRepository cityRepository,
    IWeatherApiClient weatherApiClient) : IWeatherService
{
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5);
    private int _runningRequests = 0;
    
    private async Task<List<City>> FetchCitiesAsync()
    {
        var cities = await cityRepository.Read();
        Console.WriteLine("number of cities:" + cities.Count);
        if(cities == null)throw new ArgumentNullException(nameof(cities));
        return cities;
    }

    private async Task FetchCitiesCoordinates(City cityToGetCoordinates)
    {
        var apiKey = "869e641967132b11c26a9d63f98d8270";
        var geoUrl =
            $"http://api.openweathermap.org/geo/1.0/direct?q={cityToGetCoordinates.Name}&limit=1&appid={apiKey}";
        
    }

    private async Task<WeatherResponse> GetWeatherInfoAsync(City city, CancellationToken tokenSourceToken)
    {
        var weatherInfo = await weatherApiClient.GetWeatherAsync(city.Name, tokenSourceToken);
        return weatherInfo;
    }
    
    public async Task<List<WeatherResponse>> Fetch()
    {
        List<WeatherResponse> citiesWeatherInfos = new List<WeatherResponse>();
        var cities = await FetchCitiesAsync();
        
        if (cities == null) throw new ArgumentNullException(nameof(cities));

        var tasks = cities.Select(async city =>
        {
            await _semaphore.WaitAsync(_tokenSource.Token);

            try
            {
                return await GetWeatherInfoAsync(city, _tokenSource.Token);
            }
            finally
            {
                _semaphore.Release();
            }
        });

        var results = await Task.WhenAll(tasks);

        return results.ToList();
    }
}