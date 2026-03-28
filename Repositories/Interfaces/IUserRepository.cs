
using HackathanChecker.Models;

namespace HackathanChecker.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveAsync();
    }
}