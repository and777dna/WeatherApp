using System.Text.Json.Serialization;

namespace WeatherApp.Models;

public class WeatherResponse
{
    [JsonPropertyName("name")]
    public string CityName { get; set; }

    [JsonPropertyName("main")]
    public MainInfo Main { get; set; }

    [JsonPropertyName("weather")]
    public WeatherInfo[] Weather { get; set; }

    // Удобное свойство для температуры
    [JsonIgnore]
    public double Temperature => Main?.Temp ?? 0;

    // Удобное свойство для описания погоды
    [JsonIgnore]
    public string Description => Weather != null && Weather.Length > 0
        ? Weather[0].Description
        : string.Empty;
}

public class MainInfo
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }
}

public class WeatherInfo
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}