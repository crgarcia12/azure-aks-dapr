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
        var request = daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "daprapi", "Counter");
        Console.WriteLine("Request: " + request);
        
        int counter = await daprClient.InvokeMethodAsync<int>(request);
        Console.WriteLine("Counter: " + counter);

        return counter;
    }
}
