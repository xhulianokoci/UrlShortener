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
        _context = context ?? throw new ArgumentNullException(nameof(context));
        if (_context == null)
            throw new InvalidOperationException("Database context is not initialized.");
    }

    public async Task AddAsync(ShortUrl shortUrl)
    {
        await _context.ShortUrls.AddAsync(shortUrl); ;
        await _context.SaveChangesAsync();
    }

    public async Task<ShortUrl?> GetByLongUrlAsync(string longUrl)
    {
        return await _context.ShortUrls.FirstOrDefaultAsync(x => x.Link == longUrl);
    }

    public async Task<bool> ExistsByShortLinkAsync(string shortLink)
    {
        return await _context.ShortUrls.AnyAsync(x => x.ShortLink == shortLink);
    }

    public async Task<List<ShortUrl>> GetAllAsync()
    {
        return await _context.ShortUrls.ToListAsync();
    }
}