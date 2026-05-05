using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Repositories;

public class SqlCityRepository(WeatherDbContext dbContext): ICityRepository
{
    private readonly WeatherDbContext _dbContext = dbContext;
    public List<City> Read()
    {
        throw new NotImplementedException();
    }
}