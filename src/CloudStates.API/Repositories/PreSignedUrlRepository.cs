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

        public async Task<bool> RemoveAsync(string fileKey)
        {
            PreSignedUrl? presignedUrl = _db.PreSignedUrls.Where(url => url.Key == fileKey).FirstOrDefault();

            if (presignedUrl != null)
            {
                _db.Remove(presignedUrl);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
