using Application.IRepository;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly ApplicationDbContext _context;

    public ShortUrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ShortUrl shortUrl)
    {
        await _context.ShortUrls.AddAsync(shortUrl); ;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByShortUrl(string? shortUrl)
    {
        return await _context.ShortUrls.AnyAsync(x => x.ShortLink == shortUrl);
    }
}
