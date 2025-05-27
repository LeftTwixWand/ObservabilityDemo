using Microsoft.EntityFrameworkCore;
using ObservabilityDemo.Database.Models;

namespace ObservabilityDemo.Database;

public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        WeatherForecast[] forecast = GenerateDefaultWeather();

        modelBuilder.Entity<WeatherForecast>().HasData(forecast);
    }

    private static WeatherForecast[] GenerateDefaultWeather()
    {
        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            index,
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        return forecast;
    }
}