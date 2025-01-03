using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlService _shortUrlService;

    public ShortUrlController(IShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }


    [HttpPost(Name = "GetShortUrl")]
    public async Task<IActionResult> CreateShortUrl([FromBody] LongUrlRequestDTO requestDTO)
    {
        if (string.IsNullOrWhiteSpace(requestDTO.LongUrl))
            return BadRequest("Link cannot be empty.");

        var shortUrl = await _shortUrlService.GenerateUniqueShortUrl(requestDTO.LongUrl);
        
        var response = new ShortUrlResponseDTO { Url = requestDTO.LongUrl, ShortUrl = shortUrl };
        
        return Ok(response);
    }
}
