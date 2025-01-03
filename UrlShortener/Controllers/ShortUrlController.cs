using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly ILogger<ShortUrlController> _logger;

        public ShortUrlController(ILogger<ShortUrlController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetShortUrl")]
        public ShortUrl Get()
        {
            return new ShortUrl();
        }
    }
}
