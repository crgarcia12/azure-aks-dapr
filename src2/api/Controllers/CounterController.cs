using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class Countercontroller : ControllerBase
{
   private readonly ILogger<WeatherForecastController> _logger;

    public Countercontroller(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public async Task<int> Get()
    {
        // DAPR State Management
        _logger.LogInformation($"{nameof(Countercontroller)}.{nameof(Get)} entering");

        try
        {
            const string storeName = "statestore";
            const string key = "counter";

            var daprClient = new DaprClientBuilder().Build();
            var counter = await daprClient.GetStateAsync<int>(storeName, key);
            await daprClient.SaveStateAsync(storeName, key, counter + 1);
            return counter;       
        }
        catch(Exception ex)
        {
            _logger.LogError($"{nameof(Countercontroller)}.{nameof(Get)} error: {ex.Message}");
            throw;
        }
    }
}
