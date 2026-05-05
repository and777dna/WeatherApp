using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces;

public interface IWeatherService
{
    public Task<List<WeatherResponse>> Fetch();
}