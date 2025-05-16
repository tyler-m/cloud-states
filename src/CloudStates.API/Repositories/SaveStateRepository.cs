using Microsoft.EntityFrameworkCore;
using CloudStates.API.Data;
using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    internal class SaveStateRepository(CloudStatesDbContext _db) : ISaveStateRepository
    {
        public async Task<SaveState?> AddAsync(SaveState saveState)
        {
            await _db.SaveStates.AddAsync(saveState);

            if (await _db.SaveChangesAsync() > 0)
            {
                return saveState;
            }

            return null;
        }

        public async Task<SaveState?> GetLatestAsync(int userId, string romHash, int slot)
        {
            return await _db.SaveStates
                .Where(ss => ss.UserId == userId && ss.RomHash == romHash && ss.Slot == slot)
                .OrderByDescending(ss => ss.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
