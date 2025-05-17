using CloudStates.API.Dtos;
using CloudStates.API.Exceptions;
using CloudStates.API.Models;
using CloudStates.API.Options;
using CloudStates.API.Repositories;

namespace CloudStates.API.Services
{
    internal class SaveStateService(
        SaveStateOptions _options,
        ISaveStateRepository _saveStates,
        ISaveStateFileRepository _saveStateFiles,
        IPreSignedUrlRepository _preSignedUrls
        ) : ISaveStateService
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
                throw new PersistenceException($"Unable to add pre-signed URL with key '{key}'.");
            }

            SaveStateUploadUrlResponse response = new()
            {
                FileKey = preSignedUrl.Key,
                UploadUrl = preSignedUrl.Url,
                ExpiresAt = preSignedUrl.ExpiresAt
            };

            return response;
        }

        public async Task<SaveStateStoreResponse> StoreAsync(SaveStateStoreRequest request, int userId)
        {
            bool fileExists = await _saveStateFiles.ExistsAsync(request.FileKey);

            if (!fileExists)
            {
                throw new NotFoundException($"File with key '{request.FileKey}' was not found.");
            }

            SaveState saveState = new()
            {
                RomHash = request.RomHash,
                Slot = request.Slot,
                FileKey = request.FileKey,
                CreatedAt = DateTimeOffset.UtcNow,
                UserId = userId
            };

            if (!await _preSignedUrls.RemoveAsync(request.FileKey))
            {
                throw new PersistenceException($"Unable to remove pre-signed URL with key '{request.FileKey}'.");
            }

            if (await _saveStates.AddAsync(saveState) == null)
            {
                throw new PersistenceException("Unable to store save state.");
            }

            return new SaveStateStoreResponse
            {
                RomHash = saveState.RomHash,
                Slot = saveState.Slot
            };
        }

        public async Task<SaveStateDownloadUrlResponse> GetLatestDownloadUrlAsync(SaveStateLatestRequest request, int userId)
        {
            SaveState? saveState = await _saveStates.GetLatestAsync(userId, request.RomHash, request.Slot);

            if (saveState == null)
            {
                throw new NotFoundException($"Unable to find save state with rom hash '{request.RomHash}' in slot {request.Slot}.");
            }

            string url = await _saveStateFiles.GetDownloadUrlAsync(saveState.FileKey, DateTime.UtcNow.AddMinutes(15));

            SaveStateDownloadUrlResponse response = new()
            {
                FileUrl = url
            };

            return response;
        }

        public async Task<SaveStateResponse> GetLatestAsync(SaveStateLatestRequest request, int userId)
        {
            SaveState? saveState = await _saveStates.GetLatestAsync(userId, request.RomHash, request.Slot);

            if (saveState == null)
            {
                throw new NotFoundException($"Unable to find save state with rom hash '{request.RomHash}' in slot {request.Slot}.");
            }

            return new SaveStateResponse
            {
                Id = saveState.Id,
                RomHash = saveState.RomHash,
                Slot = saveState.Slot,
                FileKey = saveState.FileKey,
                CreatedAt = saveState.CreatedAt
            };
        }
    }
}
