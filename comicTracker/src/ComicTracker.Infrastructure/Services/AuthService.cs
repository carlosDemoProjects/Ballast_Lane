using ComicTracker.Application.DTOs.Auth;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;

namespace ComicTracker.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.UserExistsAsync(dto.Email))
                throw new InvalidOperationException("Email already exist.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = User.Create(dto.FirstName, dto.LastName, dto.Email, passwordHash);

            await _userRepository.AddAsync(user);

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email)
                ?? throw new UnauthorizedAccessException("Invalid credentials.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}
