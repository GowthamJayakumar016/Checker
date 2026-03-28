
using HackathanChecker.DTOs;
using HackathanChecker.Repositories.Interfaces;
using HackathanChecker.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using HackathanChecker.DTOs;
using HackathanChecker.Models;
using HackathanChecker.Repositories.Interfaces;
using HackathanChecker.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HacakthanChecker.DTOs;

namespace HackathanChecker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _repo.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            await _repo.AddUserAsync(user);
            await _repo.SaveAsync();

            return true;
        }

      
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetUserByEmailAsync(dto.Email);

            if (user == null)
                return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                return null;

            return new LoginResponseDto
            {
                Token = GenerateToken(user),
                Role = user.Role
            };
        }

        // 🔑 JWT TOKEN
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}