using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Dapr.Demo.OrderingService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromQuery]int productId,[FromQuery]int qty)
        {
            var avalableQuantity= await _daprClient.InvokeMethodAsync<int>(
                httpMethod: HttpMethod.Get,
                appId: "inventory-service",
                methodName: $"inventory/stock/{productId}");

            if (avalableQuantity < qty)
            {
                _logger.LogWarning("Insufficient stock for product {ProductId}. Requested: {RequestedQty}, Available: {AvailableQty}",
                    productId, qty, avalableQuantity);
                return BadRequest("Insufficient stock");
            }

            return CreatedAtAction(nameof(CreateOrder), new { productId, qty }, new { OrderId = Guid.NewGuid(), ProductId = productId, Quantity = qty });
        }

        [HttpGet]
        public async Task<IActionResult> orders()
        {
            return Ok("hello there! This is the ordering service API. You can create orders by calling the POST method on this endpoint with productId and qty as query parameters.");
        }
    }
}
