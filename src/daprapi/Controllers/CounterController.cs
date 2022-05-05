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
    public async Task<int> Get()
    {
        var daprClient = new DaprClientBuilder().Build();
        var counter = await daprClient.GetStateAsync<int>("statestore", ",likescounter");
        //await daprClient.SaveStateAsync("statestore", ",likescounter", counter + 1);

        return counter;
    }

    [HttpPost()]
    public async Task<int> Post([FromBody]int newLikes)
    {
        var daprClient = new DaprClientBuilder().Build();
        var counter = await daprClient.GetStateAsync<int>("statestore", ",likescounter");
        counter += newLikes;
        await daprClient.SaveStateAsync("statestore", ",likescounter", counter);

        return counter;
    }
}
