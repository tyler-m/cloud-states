using CloudStates.API.Options;
using CloudStates.API.Repositories;

namespace CloudStates.API.Services
{
    public class CleanupService(
        CleanupOptions _options,
        IServiceProvider _services,
        ILogger<CleanupService> _logger
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _services.CreateScope())
                {
                    IPreSignedUrlRepository scopedPreSignedUrlService = scope.ServiceProvider.GetRequiredService<IPreSignedUrlRepository>();

                    try
                    {
                        _logger.LogInformation("Running pre-signed URL cleanup at {Time}.", DateTimeOffset.UtcNow);

                        //todo: delete file from filerepository if it exists,
                        int count = await scopedPreSignedUrlService.RemoveExpiredUrlsAsync(); // then remove from presignedurlrepository

                        _logger.LogInformation("{Count} pre-signed URLs removed.", count);
                    }
                    catch (TaskCanceledException)
                    {
                        _logger.LogWarning("Pre-signed URL cleanup task cancelled.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred during pre-signed URL cleanup.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(_options.MinutesBetweenCleanup), cancelToken);
            }
        }
    }
}
