using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    public interface ISaveStateRepository
    {
        Task<SaveState?> AddAsync(SaveState saveState);
    }
}
