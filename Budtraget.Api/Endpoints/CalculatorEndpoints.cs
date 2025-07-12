using Budtraget.Api.Services;

namespace Budtraget.Api.Endpoints;

public static class CalculatorEndpoints
{
    public static void MapCalculatorEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/calculator");

        group.MapGet("/sum", SumHandler)
            .WithName("CalculatorSum")
            .WithSummary("Returns the sum of two numbers")
            .WithDescription("Uses ICalculatorService to compute sum of a and b.");
    }

    private static IResult SumHandler(
        long a,
        long b,
        ICalculatorService calculator,
        ILogger<CalculatorService> logger)
    {
        logger.LogInformation("Calling SumUp from endpoint with a = {A}, b = {B}", a, b);
        var result = calculator.Sumup(a, b);
        return Results.Ok(new { a, b, sum = result });
    }
}