using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AddAsync(User user);
        Task<bool> UserWithUsernameExists(string username);
    }
}
