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
        var correlationId = Guid.NewGuid().ToString(); // simulate or fetch from request header

        using (logger.BeginScope(new List<KeyValuePair<string, object>>
               {
                   new("CorrelationId", correlationId),
                   new("Endpoint", "SumHandler")
               }))
        {
            logger.LogInformation("Calling SumUp from endpoint with a = {A}, b = {B}", a, b);
            var result = calculator.Sumup(a, b);
            logger.LogInformation("Sum result: {Result}", result);

            return Results.Ok(new { a, b, sum = result, correlationId });
        }

    }
}