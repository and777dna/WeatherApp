namespace WeatherApp.Models;

public class WeatherResponse
{
    public string CityName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<WeatherRecord> WeatherRecords { get; set; } = new();
}