namespace Budtraget.Api.Services;

public class CalculatorService(ILogger<CalculatorService> logger) : ICalculatorService
{
    ILogger<CalculatorService> Logger { get; } = logger;

    public double Sumup(long a, long b)
    {
        // log request
        Logger.LogInformation("Summing {L} + {L1} = {L2}", a, b, a + b);
        return a + b;
    }
}