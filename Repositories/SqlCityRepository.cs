using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Repositories;

public class SqlCityRepository(WeatherDbContext dbContext): ICityRepository
{
    private readonly WeatherDbContext _dbContext = dbContext;
    public async Task<List<City>> Read()
    {
        return await _dbContext.Cities.ToListAsync();
    }
    public async Task Create(WeatherRecord weatherRecord)
    {
        await _dbContext.WeatherRecords.AddAsync(weatherRecord);
        await _dbContext.SaveChangesAsync();
    }
    public Task Update()
    {
        return Task.CompletedTask;
    }
}