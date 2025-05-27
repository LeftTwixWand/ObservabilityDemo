using ObservabilityDemo.Database.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
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
})
.WithName("GetWeatherForecast");

#region Database
//builder.AddNpgsqlDbContext<WeatherDbContext>("weatherdb");

//app.MapGet("/weatherforecast", async (WeatherDbContext dbContext, CancellationToken cancellationToken) =>
//{
//    // Load existing forecasts from the database
//    var forecast = await dbContext.WeatherForecasts.ToArrayAsync(cancellationToken);

//    return forecast;
//})
//.WithName("GetWeatherForecast");

//app.MapGet("/migrate", async (WeatherDbContext dbContext, CancellationToken cancellationToken) =>
//{
//    var created = await dbContext.Database.EnsureCreatedAsync(cancellationToken);

//    return created;
//})
//.WithName("Migrate");
#endregion

app.MapDefaultEndpoints();

app.Run();