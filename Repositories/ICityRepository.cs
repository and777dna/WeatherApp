using WeatherApp.Models;

namespace WeatherApp.Repositories;

public interface ICityRepository
{
    public List<City> Read();
}