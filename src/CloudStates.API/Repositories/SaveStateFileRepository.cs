using Amazon.S3;
using Amazon.S3.Model;
using CloudStates.API.Options;

namespace CloudStates.API.Repositories
{
    internal class SaveStateFileRepository(AmazonS3Client _s3, S3Options _options) : ISaveStateFileRepository
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
    }
}
