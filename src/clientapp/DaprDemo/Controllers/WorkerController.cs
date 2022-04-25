// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSchedulerApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Dapr;
    using Dapr.Client;

    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        // GET: api/<WorkerController>
        [HttpPost("checkout")]
        [Topic("order_pub_sub", "orders")]
        public string Post([FromBody] string orderId)
        {
            Console.WriteLine("Subscriber received : " + orderId);
            return "received: " + orderId;
        }

    }
}
