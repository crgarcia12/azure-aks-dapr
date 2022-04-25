using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DaprClient _daprClient;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        _logger.LogInformation($"LOG: {nameof(WeatherForecastController)}.{nameof(Get)} entering");

        try
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Counter = _daprClient.InvokeMethodAsync<int>(
                    HttpMethod.Get,
                    "demoapi",
                    "Counter").Result
            }).ToArray();
        } 
        catch (Exception ex)
        {
            _logger.LogError($"LOG: {nameof(WeatherForecastController)}.{nameof(Get)} Error: " + ex.Message);
            throw;
        }
    }
}
