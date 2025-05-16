using Amazon.S3;
using Amazon.S3.Model;
using CloudStates.API.Options;

namespace CloudStates.API.Repositories
{
    internal class SaveStateFileRepository(
        AmazonS3Client _s3,
        S3Options _options
        ) : ISaveStateFileRepository
    {
        public async Task<string> GetUploadUrlAsync(string fileKey, DateTime expiresAt)
        {
            GetPreSignedUrlRequest request = new()
            {
                Protocol = (Protocol)_options.Protocol,
                BucketName = _options.SaveStatesBucket,
                Key = fileKey,
                Expires = expiresAt,
                Verb = HttpVerb.PUT,
                ContentType = "application/octet-stream",
            };

            return await _s3.GetPreSignedURLAsync(request);
        }

        public async Task<string> GetDownloadUrlAsync(string fileKey, DateTime expiresAt)
        {
            GetPreSignedUrlRequest request = new()
            {
                Protocol = (Protocol)_options.Protocol,
                BucketName = _options.SaveStatesBucket,
                Key = fileKey,
                Expires = expiresAt,
                Verb = HttpVerb.GET
            };

            return await _s3.GetPreSignedURLAsync(request);
        }

        public async Task<bool> ExistsAsync(string fileKey)
        {
            ListObjectsV2Response response = await _s3.ListObjectsV2Async(
                new ListObjectsV2Request()
                {
                    Prefix = fileKey,
                    BucketName = _options.SaveStatesBucket,
                    MaxKeys = 1 // we generate GUIDs for file keys, this should be safe
                });

            if (response.S3Objects == null)
            {
                return false;
            }

            return response.S3Objects.Any(o => o.Key == fileKey);
        }
    }
}
