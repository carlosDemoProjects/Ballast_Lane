using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
