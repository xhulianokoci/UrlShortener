using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlService _service;

    public ShortUrlController(IShortUrlService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost(Name = "CreatShortUrl")]
    public async Task<IActionResult> CreateShortUrl([FromBody] LongUrlRequestDTO requestDTO)
    {
        if (string.IsNullOrWhiteSpace(requestDTO.LongUrl))
            return BadRequest("Long URL cannot be empty.");

        // Validate URL format
        if (!Uri.TryCreate(requestDTO.LongUrl, UriKind.Absolute, out var validatedUri) ||
        (validatedUri.Scheme != Uri.UriSchemeHttp && validatedUri.Scheme != Uri.UriSchemeHttps))
        {
            return BadRequest("Invalid URL format. Only HTTP and HTTPS are allowed.");
        }

        try
        {
            var shortUrl = await _service.GenerateUniqueShortUrl(requestDTO.LongUrl);
            var response = new ShortUrlResponseDTO { Url = requestDTO.LongUrl, ShortUrl = shortUrl };
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}
