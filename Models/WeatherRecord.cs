namespace WeatherApp.Models;

public class WeatherRecord
{
    public int Id { get; set; }

    public string CityName { get; set; }

    public double Temperature { get; set; }

    public string Description { get; set; }

    public DateTime WeatherDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public City City { get; set; }
}