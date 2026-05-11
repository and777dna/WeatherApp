using WeatherApp.Models;

namespace WeatherApp.Repositories;

public interface IWeatherRecordRepository
{
    public Task<List<WeatherRecord>> Read();
    public Task Create(WeatherRecord weatherRecord);
}