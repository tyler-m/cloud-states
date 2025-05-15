using CloudStates.API.Dtos;

namespace CloudStates.API.Services
{
    public interface ISaveStateService
    {
        Task<SaveStateUploadUrlResponse> GetUploadUrlAsync();
    }
}
