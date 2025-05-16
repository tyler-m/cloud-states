using CloudStates.API.Dtos;

namespace CloudStates.API.Services
{
    public interface ISaveStateService
    {
        Task<SaveStateUploadUrlResponse> GetUploadUrlAsync();
        Task<SaveStateStoreResponse> StoreAsync(SaveStateStoreRequest request, int userId);
        Task<SaveStateDownloadUrlResponse> GetDownloadUrlAsync(SaveStateDownloadUrlRequest request, int userId);
    }
}
