using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WeatherApp.Data;

public class WeatherDbContextFactory : IDesignTimeDbContextFactory<WeatherDbContext>
{
    public WeatherDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();

        optionsBuilder.UseMySql(
            "Server=localhost;Database=mydb;User=root;Password=my-secret;Port=3308;",
            ServerVersion.AutoDetect("Server=localhost;Database=mydb;User=root;Password=my-secret;Port=3308;")
        );

        return new WeatherDbContext(optionsBuilder.Options);
    }
}