using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories
{
    public class ComicRepository : IComicRepository
    {
        private readonly AppDbContext _context;

        public ComicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comic>> GetAllByUserAsync(Guid userId)
        {
            return await _context.Comics
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    


        public async Task<Comic?> GetByIdAsync(Guid id) =>
            await _context.Comics.FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Comic comic)
        {
            await _context.Comics.AddAsync(comic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comic comic)
        {
            _context.Comics.Update(comic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic != null)
            {
                _context.Comics.Remove(comic);
                await _context.SaveChangesAsync();
            }
        }
    }
}
