using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace Dapr.Demo.Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        private static ConcurrentDictionary<int, int> PRODUCT_PAIR_STOCK = new ConcurrentDictionary<int, int>
        {
            [1] = 100,
            [2] = 200,
            [3] = 300
        };
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("stock/{productId}")]
        public async Task<ActionResult<int>> Get([FromRoute] int productId)
        {
            return Ok(22);
        }
    }
}
