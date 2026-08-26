using ComicTracker.Application.DTOs.Comics;

namespace ComicTracker.Application.Interfaces
{
    public interface IComicService
    {
        Task<IEnumerable<ComicDto>> GetAllAsync(Guid userId);
        Task<ComicDto?> GetByIdAsync(Guid id, Guid userId);
        Task<ComicDto> CreateAsync(CreateUpdateComicDto dto, Guid userId);
        Task<ComicDto> UpdateAsync(Guid id, CreateUpdateComicDto dto, Guid userId);
        Task DeleteAsync(Guid id, Guid userId);
    }
}
