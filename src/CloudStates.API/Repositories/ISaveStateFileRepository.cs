
namespace CloudStates.API.Repositories
{
    public interface ISaveStateFileRepository
    {
        Task<string> GetUploadUrlAsync(string fileKey, DateTime expiresAt);
        Task<string> GetDownloadUrlAsync(string fileKey, DateTime expiresAt);
        Task<bool> ExistsAsync(string fileKey);
    }
}
