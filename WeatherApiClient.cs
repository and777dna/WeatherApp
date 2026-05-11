using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp;

public class WeatherApiClient : IWeatherApiClient
{
    public async Task<WeatherResponse> GetWeatherAsync(string cityName, CancellationToken ct)
    {
      var result = new WeatherResponse
      {
        CityName = cityName
      };
        var apiKey = "869e641967132b11c26a9d63f98d8270";
        //var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric&lang=ru";
        
        var geoUrl =
          $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}";
      
        
        var httpClient = new HttpClient();
        
        
        var responseGeo = await httpClient.GetAsync(geoUrl, ct);
        
        var jsonGeo = await responseGeo.Content.ReadAsStringAsync();
        var geo = JsonDocument.Parse(jsonGeo);
        double lat = geo.RootElement[0].GetProperty("lat").GetDouble();
        double lon = geo.RootElement[0].GetProperty("lon").GetDouble();
        Console.WriteLine("jsonGeo:" + jsonGeo + " " + lat + " " + lon);
        result.Latitude = lat;
        result.Longitude = lon;


        for (int i = 1; i <= 3; i++)
        {
          var date = DateTime.UtcNow.Date.AddDays(-i).AddHours(12);
          long unixTime = ((DateTimeOffset)date).ToUnixTimeSeconds();
          var weatherUrl =
            $"https://api.openweathermap.org/data/3.0/onecall/timemachine" +
            $"?lat={lat}&lon={lon}&dt={unixTime}&appid={apiKey}&units=metric&lang=ru";
          var responseWeather = await httpClient.GetAsync(weatherUrl, ct);
          var jsonWeather = await responseWeather.Content.ReadAsStringAsync(ct);
          var doc = JsonDocument.Parse(jsonWeather);
          Console.WriteLine("weath:" + doc.RootElement.ToString());
          var current = doc.RootElement.GetProperty("data")[0];

          result.Days.Add(new WeatherDay
          {
            Date = date,
            Temperature = current.GetProperty("temp").GetDouble(),
            Description = current.GetProperty("weather")[0]
              .GetProperty("description").GetString()
          });
        }

        return result;
        /*string json = """
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
        if (responseGeo.IsSuccessStatusCode)
        {
          json = await responseGeo.Content.ReadAsStringAsync(ct);
        }
        
        var weather = JsonSerializer.Deserialize<WeatherResponse>(json);
        
        return weather;*/
    }
}