using Dapr.Sample.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dapr.Sample.WebApi.Controllers
{
    [ApiController]
    public class PubSubMessagingController : ControllerBase
    {
        private readonly ILogger _logger;

        public PubSubMessagingController(ILogger<PubSubMessagingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/order")]
        public async Task<IActionResult> ReceiveOrder([FromBody]Order order)
        {
            _logger.LogInformation($"Order with id {order.Id} received!");

            //Validate order placeholder

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(
                    "http://localhost:5001/v1.0/publish/ordertopic",
                    new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json")
                );

                _logger.LogInformation($"Order with id {order.Id} published with status {result.StatusCode}!");
            }

            return Ok();
        }

        [Topic("ordertopic")]
        [HttpPost]
        [Route("ordertopic")]
        public async Task<IActionResult> ProcessOrder([FromBody]Order order)
        {
            await Task.Delay(0);

            //Process order placeholder

            _logger.LogInformation($"Order with id {order.Id} processed!");
            return Ok();
        }
    }
}
