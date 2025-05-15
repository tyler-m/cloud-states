using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    public interface IPreSignedUrlRepository
    {
        Task<bool> AddAsync(PreSignedUrl preSignedUrl);
        Task<bool> RemoveAsync(string fileKey);
    }
}
