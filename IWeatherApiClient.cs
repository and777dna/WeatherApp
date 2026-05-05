using WeatherApp.Models;

namespace WeatherApp;

public interface IWeatherApiClient
{
    public Task<WeatherResponse?> GetWeatherAsync(string cityName, CancellationToken ct);
}