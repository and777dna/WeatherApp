using WeatherApp.Models;

namespace WeatherApp.Repositories;

public interface ICityRepository
{
    public Task<List<City>> Read();
    public Task Create(WeatherRecord weatherRecord);
    public Task Update();
}