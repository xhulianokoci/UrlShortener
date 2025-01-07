using Application.Configuration;
using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text;

namespace Application.Services;

public class ShortUrlService : IShortUrlService
{
    private readonly IShortUrlRepository _shortUrlRepository;
    private readonly ShortUrlSettings _shortUrlDomain;

    public ShortUrlService(IShortUrlRepository shortUrlRepository, IOptions<ShortUrlSettings> options)
    {
        _shortUrlRepository = shortUrlRepository ?? throw new ArgumentNullException(nameof(shortUrlRepository));
        _shortUrlDomain = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<string> GenerateUniqueShortUrl(string longUrl)
    {
        var existingShortUrl = await _shortUrlRepository.GetByLongUrlAsync(longUrl);
        if (existingShortUrl != null)
        {
            return $"{_shortUrlDomain.BaseUrl}/{existingShortUrl.ShortLink}";
        }

        if (string.IsNullOrWhiteSpace(_shortUrlDomain.BaseUrl))
            throw new InvalidOperationException("Base URL is not configured.");
        
        string shortGeneratedUrl;

        do
        {
            shortGeneratedUrl = GenerateRandomString(7);
        }
        while (await _shortUrlRepository.ExistsByShortLinkAsync(shortGeneratedUrl));

        var shortUrl = new ShortUrl
        {
            Link = longUrl,
            ShortLink = shortGeneratedUrl,
            DateCreated = DateTime.Now
        };

        await _shortUrlRepository.AddAsync(shortUrl);

        return $"{_shortUrlDomain.BaseUrl}/{shortGeneratedUrl}";
    }

    public async Task<string?> GetLongUrlByShortCodeAsync(string shortCode)
    {
        var shortUrl = await _shortUrlRepository.GetByShortLinkAsync(shortCode);

        if(shortUrl == null)
        {
            return null;
        }

        return shortUrl.Link;
    }


    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private string GenerateRandomString(int length)
    {
        var random = new Random();
        var shortUrl = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            shortUrl.Append(Characters[random.Next(Characters.Length)]);
        }

        return shortUrl.ToString();
    }
}