using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces
{
    public interface IComicRepository
    {
        Task<IEnumerable<Comic>> GetAllByUserAsync(Guid userId); 
        Task<Comic?> GetByIdAsync(Guid id);
        Task AddAsync(Comic comic);
        Task UpdateAsync(Comic comic);
        Task DeleteAsync(Guid id);
    }
}
