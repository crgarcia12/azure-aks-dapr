using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace daprapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public CounterController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public async Task<int> Get()
    {
        var daprClient = new DaprClientBuilder().Build();
        var counter = await daprClient.GetStateAsync<int>("statestore", ",maincounter");

        await daprClient.SaveStateAsync("statestore", ",maincounter", counter + 1);

        return counter;

    }
}
