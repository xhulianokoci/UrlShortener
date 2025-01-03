using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using System.Text;

namespace Application.Services;

public class ShortUrlService : IShortUrlService
{
    private readonly IShortUrlRepository _shortUrlRepository;

    public ShortUrl GetShortUrl(string shortUrl)
    { 
        return new ShortUrl();
    }

    public async Task<string> GenerateUniqueShortUrl(ShortUrl shortUrl)
    {
        string shortGeneratedUrl;

        do
        {
            shortGeneratedUrl = GenerateRandomString(7);

        } while (await _shortUrlRepository.ExistsByShortUrl(shortGeneratedUrl));

        return shortGeneratedUrl;
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
