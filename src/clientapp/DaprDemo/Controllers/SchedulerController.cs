// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSchedulerApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Dapr;
    using Dapr.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        const string StoreName = "statestore";
        const string Key = "stateKey";

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<int> Get()
        {
            var daprClient = new DaprClientBuilder().Build();
            int counter = await daprClient.GetStateAsync<int>(StoreName, Key);

            await daprClient.SaveStateAsync(StoreName, Key, ++counter);

            return counter;
        }

    }
}
