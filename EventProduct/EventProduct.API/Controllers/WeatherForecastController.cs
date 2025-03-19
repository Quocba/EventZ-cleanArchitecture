using Base.API;
using Microsoft.AspNetCore.Mvc;

namespace EventProduct.API.Controllers
{
    [ApiController]
    [Route("api/event-product-weather")]
    public class WeatherForecastController : BaseAPIController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "Event Product Weather" });
        }
    }
}
