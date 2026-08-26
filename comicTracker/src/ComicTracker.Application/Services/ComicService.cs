using ComicTracker.Application.DTOs.Comics;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;

namespace ComicTracker.Application.Services
{
    public class ComicService : IComicService
    {
        private readonly IComicRepository _repository;

        public ComicService(IComicRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ComicDto>> GetAllAsync(Guid userId)
        {
            var comics = await _repository.GetAllByUserAsync(userId);
            return comics.Select(MapToDto);
        }

        public async Task<ComicDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var comic = await _repository.GetByIdAsync(id);
            if (comic is null || comic.UserId != userId) return null;
            return MapToDto(comic);
        }

        public async Task<ComicDto> CreateAsync(CreateUpdateComicDto dto, Guid userId)
        {
            var comic = Comic.Create(dto.Title, dto.Writer, dto.Artist, dto.Publisher, userId, dto.Readed);
            await _repository.AddAsync(comic);
            return MapToDto(comic);
        }

        public async Task<ComicDto> UpdateAsync(Guid id, CreateUpdateComicDto dto, Guid userId)
        {
            var comic = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Comic {id} not found.");

            if (comic.UserId != userId)
                throw new UnauthorizedAccessException("You don't own this comic.");

            comic.Update(dto.Title, dto.Writer, dto.Artist, dto.Publisher);

            if (dto.Readed) 
                comic.MarkAsRead();                        

            await _repository.UpdateAsync(comic);
            return MapToDto(comic);
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var comic = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Comic {id} not found.");

            if (comic.UserId != userId)
                throw new UnauthorizedAccessException("You don't own this comic.");

            await _repository.DeleteAsync(comic.Id);
        }

        private static ComicDto MapToDto(Comic comic) => new()
        {
            Id = comic.Id,
            Title = comic.Title,
            Writer = comic.Writer,
            Artist = comic.Artist,
            Publisher = comic.Publisher,
            Readed = comic.Readed,
            CreatedAt = comic.CreatedAt
        };
    }
}
