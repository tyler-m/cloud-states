using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    public interface ISaveStateRepository
    {
        Task<SaveState?> AddAsync(SaveState saveState);
        Task<SaveState?> GetLatestAsync(int userId, string romHash, int slot);
    }
}
