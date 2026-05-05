using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services;

public class WeatherService(ICityRepository cityRepository,
    IWeatherApiClient weatherApiClient) : IWeatherService
{
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5);
    private int _runningRequests = 0;
    
    private List<City> FetchCities()
    {
        var cities = cityRepository.Read();
        if(cities == null)throw new ArgumentNullException(nameof(cities));
        return cities;
    }

    private async Task<WeatherResponse> GetWeatherInfoAsync(City city, CancellationToken tokenSourceToken)
    {
        var weatherInfo = await weatherApiClient.GetWeatherAsync(city.Name, tokenSourceToken);
        return weatherInfo;
    }
    
    public async Task<List<WeatherResponse>> Fetch()
    {
        List<WeatherResponse> citiesWeatherInfos = new List<WeatherResponse>();
        var cities = FetchCities();
        
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