using Domain.Entities;

namespace Application.IRepository;

public interface IShortUrlRepository
{
    Task AddAsync(ShortUrl shortUrl);
    Task<ShortUrl> FindByLongUrl(string? longUrl);
    Task<bool> ExistsByShortUrl(string? shortUrl);
}
