using HacakthanChecker.DTOs;
using HackathanChecker.DTOs;
using HackathanChecker.DTOs;

namespace HackathanChecker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}