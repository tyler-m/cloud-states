using CloudStates.API.Dtos;

namespace CloudStates.API.Services
{
    public interface ISaveStateService
    {
        Task<SaveStateUploadUrlResponse> GetUploadUrlAsync();
        Task<SaveStateStoreResponse> StoreAsync(SaveStateStoreRequest request, int userId);
        Task<SaveStateDownloadUrlResponse> GetLatestDownloadUrlAsync(SaveStateLatestRequest request, int userId);
        Task<SaveStateResponse> GetLatestAsync(SaveStateLatestRequest request, int userId);
    }
}
