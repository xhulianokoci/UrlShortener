namespace Application.Interfaces;

public interface IShortUrlService
{
    Task<string> GenerateUniqueShortUrl(string longUrl);
    Task<string?> GetLongUrlByShortCodeAsync(string shortCode);
}