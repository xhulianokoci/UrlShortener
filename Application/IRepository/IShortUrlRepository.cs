using Domain.Entities;

namespace Application.IRepository;

public interface IShortUrlRepository
{
    Task AddAsync(ShortUrl shortUrl);
    Task<bool> ExistsByShortLinkAsync(string? shortUrl);
    Task<ShortUrl?> GetByLongUrlAsync(string longUrl);
    Task<List<ShortUrl>> GetAllAsync();
}
