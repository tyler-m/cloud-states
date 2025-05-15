using CloudStates.API.Dtos;
using CloudStates.API.Models;
using CloudStates.API.Options;
using CloudStates.API.Repositories;

namespace CloudStates.API.Services
{
    internal class SaveStateService(SaveStateOptions _options, IPreSignedUrlRepository _preSignedUrls, ISaveStateFileRepository _saveStateFiles) : ISaveStateService
    {
        public async Task<SaveStateUploadUrlResponse> GetUploadUrlAsync()
        {
            string extension = _options.FileExtension;
            double minutes = _options.MinutesUntilUploadUrlExpires;

            string key = $"{Guid.NewGuid():N}.{extension}";
            DateTime expiresAt = DateTime.UtcNow.AddMinutes(minutes);

            string url = await _saveStateFiles.GetUploadUrlAsync(key, expiresAt);

            PreSignedUrl preSignedUrl = new()
            {
                Key = key,
                Url = url,
                ExpiresAt = expiresAt
            };

            if (!await _preSignedUrls.AddAsync(preSignedUrl))
            {
                throw new Exception($"Unable to add pre-signed URL with key '{key}'.");
            }

            SaveStateUploadUrlResponse response = new()
            {
                FileKey = preSignedUrl.Key,
                UploadUrl = preSignedUrl.Url,
                ExpiresAt = preSignedUrl.ExpiresAt
            };

            return response;
        }
    }
}
