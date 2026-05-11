using WeatherApp.Models;

namespace WeatherApp.Repositories;

public class CityRepository : ICityRepository
{
    private readonly List<City> _cities = new List<City>
    {
        new City { Id = 1, Name = "Washington" },
        new City { Id = 2, Name = "London" },
        new City { Id = 3, Name = "Paris" },
        new City { Id = 4, Name = "Berlin" },
        new City { Id = 5, Name = "Madrid" },
        new City { Id = 6, Name = "Rome" },
        new City { Id = 7, Name = "Ottawa" },
        new City { Id = 8, Name = "Canberra" },
        new City { Id = 9, Name = "Tokyo" },
        new City { Id = 10, Name = "Beijing" },
        new City { Id = 11, Name = "Seoul" },
        new City { Id = 12, Name = "New Delhi" },
        new City { Id = 13, Name = "Brasilia" },
        new City { Id = 14, Name = "Buenos Aires" },
        new City { Id = 15, Name = "Mexico City" },
        new City { Id = 16, Name = "Cairo" },
        new City { Id = 17, Name = "Ankara" },
        new City { Id = 18, Name = "Moscow" },
        new City { Id = 19, Name = "Kyiv" },
        new City { Id = 20, Name = "Warsaw" },
        new City { Id = 21, Name = "Prague" },
        new City { Id = 22, Name = "Vienna" },
        new City { Id = 23, Name = "Budapest" },
        new City { Id = 24, Name = "Brussels" },
        new City { Id = 25, Name = "Amsterdam" },
        new City { Id = 26, Name = "Copenhagen" },
        new City { Id = 27, Name = "Stockholm" },
        new City { Id = 28, Name = "Oslo" },
        new City { Id = 29, Name = "Helsinki" },
        new City { Id = 30, Name = "Dublin" },
        new City { Id = 31, Name = "Lisbon" },
        new City { Id = 32, Name = "Athens" },
        new City { Id = 33, Name = "Bern" },
        new City { Id = 34, Name = "Bangkok" },
        new City { Id = 35, Name = "Jakarta" },
        new City { Id = 36, Name = "Kuala Lumpur" },
        new City { Id = 37, Name = "Singapore" },
        new City { Id = 38, Name = "Manila" },
        new City { Id = 39, Name = "Hanoi" },
        new City { Id = 40, Name = "Riyadh" },
        new City { Id = 41, Name = "Abu Dhabi" },
        new City { Id = 42, Name = "Doha" },
        new City { Id = 43, Name = "Tehran" },
        new City { Id = 44, Name = "Baghdad" },
        new City { Id = 45, Name = "Jerusalem" },
        new City { Id = 46, Name = "Pretoria" },
        new City { Id = 47, Name = "Nairobi" },
        new City { Id = 48, Name = "Addis Ababa" },
        new City { Id = 49, Name = "Wellington" },
        new City { Id = 50, Name = "Reykjavik" }
    };
    public Task<List<City>> Read()
    {
        return Task.FromResult(_cities);
    }

    public Task Create(WeatherRecord weatherRecord)
    {
        return Task.CompletedTask;
    }

    public Task Update()
    {
        return Task.CompletedTask;
    }
}