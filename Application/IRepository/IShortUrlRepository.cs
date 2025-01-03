using Domain.Entities;

namespace Application.IRepository;

public interface IShortUrlRepository
{
    Task AddAsync(ShortUrl shortUrl);
    Task<bool> ExistsByShortUrl(string? shortUrl);
}
