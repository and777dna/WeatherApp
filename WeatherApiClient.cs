using System.Text.Json;
using WeatherApp.Data;
using WeatherApp.Models;
using WeatherApp.Repositories;

namespace WeatherApp;

public class WeatherApiClient(IWeatherRecordRepository weatherRecordRepository) : IWeatherApiClient
{
  HttpClient httpClient = new HttpClient();
  WeatherResponse result = new WeatherResponse {};
  
  private async Task<bool> CheckWeatherPerDayExists(string cityName, DateTime weatherDate)
  {
    var records = await weatherRecordRepository.Read();
    return records.Any(record => record.CityName == cityName && record.WeatherDate == weatherDate);
  }
  private async Task GetCoordinates(string geoUrl, CancellationToken ct)
  {
    var responseGeo = await httpClient.GetAsync(geoUrl, ct);
    var jsonGeo = await responseGeo.Content.ReadAsStringAsync();
    var geo = JsonDocument.Parse(jsonGeo);
    double lat = geo.RootElement[0].GetProperty("lat").GetDouble();
    double lon = geo.RootElement[0].GetProperty("lon").GetDouble();
    Console.WriteLine("jsonGeo:" + jsonGeo + " " + lat + " " + lon);
    result.Latitude = lat;
    result.Longitude = lon;
  }

  private async Task GetWeatherPerLastThreeDays(string apiKey, CancellationToken ct, string cityName)
  {
    for (int i = 1; i <= 3; i++)
    {
      var date = DateTime.UtcNow.Date.AddDays(-i).AddHours(12);
      long unixTime = ((DateTimeOffset)date).ToUnixTimeSeconds();
      var weatherUrl =
        $"https://api.openweathermap.org/data/3.0/onecall/timemachine" +
        $"?lat={result.Latitude}&lon={result.Longitude}&dt={unixTime}&appid={apiKey}&units=metric&lang=ru";
      var recordExist = await CheckWeatherPerDayExists(cityName, date);
      if(recordExist) continue;
      var responseWeather = await httpClient.GetAsync(weatherUrl, ct);
      var jsonWeather = await responseWeather.Content.ReadAsStringAsync(ct);
      var doc = JsonDocument.Parse(jsonWeather);
      Console.WriteLine("weath:" + doc.RootElement.ToString());
      var current = doc.RootElement.GetProperty("data")[0];

      var weatherDay = new WeatherRecord()
      {
        WeatherDate = date,
        Temperature = current.GetProperty("temp").GetDouble(),
        Description = current.GetProperty("weather")[0].GetProperty("description").GetString()
      };
          
      result.WeatherRecords.Add(weatherDay);
      await weatherRecordRepository.Create(weatherDay);
    }
  }
  
    public async Task<WeatherResponse> GetWeatherAsync(string cityName, CancellationToken ct)
    {
      //var result = new WeatherResponse { CityName = cityName };
        result.CityName = cityName;
        var apiKey = "869e641967132b11c26a9d63f98d8270";
        //var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric&lang=ru";
        var geoUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}";
        
        await GetCoordinates(geoUrl, ct);
        await GetWeatherPerLastThreeDays(apiKey, ct, cityName);

        return result;
    }
}