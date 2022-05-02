using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace daprapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : ControllerBase
{
    private readonly ILogger<CounterController> _logger;

    public CounterController(ILogger<CounterController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public async Task<string> Get()
    {
        var daprClient = new DaprClientBuilder().Build();
        await daprClient.SaveStateAsync("statestore", "message", "This was stored in the state!");

        return await daprClient.GetStateAsync<string>("statestore", "message");
    }
}
