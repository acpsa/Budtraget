namespace Budtraget.Api.Endpoints;

public static class WeatherEndpoints
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/weather");

        group.MapGet("/forecast", GetForecast)
            .WithName("GetWeatherForecast")
            .WithSummary("Returns a 5-day weather forecast");
    }

    private static WeatherForecast[] GetForecast()
    {
        return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
    }
}
//public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
