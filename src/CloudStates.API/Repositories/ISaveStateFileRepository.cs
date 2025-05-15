
namespace CloudStates.API.Repositories
{
    public interface ISaveStateFileRepository
    {
        Task<string> GetUploadUrlAsync(string fileKey, DateTime expiresAt);
    }
}
