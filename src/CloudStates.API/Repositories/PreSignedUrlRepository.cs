using CloudStates.API.Data;
using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    internal class PreSignedUrlRepository(CloudStatesDbContext _db) : IPreSignedUrlRepository
    {
        public async Task<bool> AddAsync(PreSignedUrl preSignedUrl)
        {
            await _db.PreSignedUrls.AddAsync(preSignedUrl);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
