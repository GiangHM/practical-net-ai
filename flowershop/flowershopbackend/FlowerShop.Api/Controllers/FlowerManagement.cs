using FlowerShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowerManagement : ControllerBase
    {
        private readonly ILogger<FlowerManagement> _logger;
        private readonly IFlowerService _flowerService;

        public FlowerManagement(ILogger<FlowerManagement> logger, IFlowerService flowerService)
        {
            _logger = logger;
            _flowerService = flowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            return Ok(flowers);
        }
    }
}
