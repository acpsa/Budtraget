using Budtraget.Api.Endpoints;
using Budtraget.Api.Services;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// Logger setup
SetupLogger(builder);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICalculatorService,CalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

// Add application endpoints
app.MapCalculatorEndpoints();
app.MapWeatherEndpoints(); 

// Run the app
app.Run();


static void SetupLogger(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog();
    
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console() // Optional
        .WriteTo.File(
            new JsonFormatter(renderMessage: true), // 👈 Pretty-printed JSON formatter
            "Logs/log.json",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 7,
            shared: true)
        .CreateLogger();
}
