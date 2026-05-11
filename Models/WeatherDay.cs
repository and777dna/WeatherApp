namespace WeatherApp.Models;

public class WeatherDay
{
    public DateTime Date { get; set; }
    public double Temperature { get; set; }
    public string? Description { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
}