using HackathanChecker.Models;

namespace HackathanChecker.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveAsync();
    }
}