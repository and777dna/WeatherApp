using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WeatherApp;
using WeatherApp.Repositories;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("/Users/andreisartin/RiderProjects/WeatherApp/WeatherApp/appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);

//services.AddHttpClient();
//services.AddLogging();
services.AddScoped<IWeatherService, WeatherService>();
services.AddScoped<IWeatherApiClient, WeatherApiClient>();
services.AddSingleton<ICityRepository, SqlCityRepository>();

var provider = services.BuildServiceProvider();

var connString = configuration.GetConnectionString("FulfilmentCenterDatabase");
Console.WriteLine(connString);


var weatherService = provider.GetRequiredService<IWeatherService>();


var result = await weatherService.Fetch();
Console.WriteLine("result.Count:" + result.Count);//we synchonize here
foreach (var city in result.OrderBy(city => city.CityName))
{
    Console.WriteLine(city.Temperature + " " + city.CityName + " " + city.Description);
}
Console.WriteLine("vjbdifjb");