using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Repositories;

public class SqlWeatherRecord(WeatherDbContext dbContext) : IWeatherRecordRepository
{
    private readonly WeatherDbContext _dbContext = dbContext;
    
    public async Task<List<WeatherRecord>> Read()
    {
        return await _dbContext.WeatherRecords.ToListAsync();
    }

    public async Task Create(WeatherRecord weatherRecord)
    {
        await _dbContext.WeatherRecords.AddAsync(weatherRecord);
    }
}