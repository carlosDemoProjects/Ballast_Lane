using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task<bool> UserExistsAsync(string email);
    }
}
