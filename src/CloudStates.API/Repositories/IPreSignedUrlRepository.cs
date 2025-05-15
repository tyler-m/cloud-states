using CloudStates.API.Models;

namespace CloudStates.API.Repositories
{
    public interface IPreSignedUrlRepository
    {
        Task<bool> AddAsync(PreSignedUrl preSignedUrl);
    }
}
