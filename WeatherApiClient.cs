using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp;

public class WeatherApiClient : IWeatherApiClient
{
    public async Task<WeatherResponse> GetWeatherAsync(string cityName, CancellationToken ct)
    {
        var apiKey = "869e641967132b11c26a9d63f98d8270";
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric&lang=ru";
        
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url, ct);
        string json = """
                          {
                            "main": {
                              "temp": 18.5
                            },
                            "weather": [
                              {
                                "description": "ясно"
                              }
                            ]
                          }
                          """;
        if (response.IsSuccessStatusCode)
        {
          json = await response.Content.ReadAsStringAsync(ct);
        }
        
        var weather = JsonSerializer.Deserialize<WeatherResponse>(json);
        
        return weather;
    }
}