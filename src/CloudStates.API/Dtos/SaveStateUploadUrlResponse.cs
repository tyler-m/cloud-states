namespace CloudStates.API.Dtos
{
    public class SaveStateUploadUrlResponse
    {
        required public string FileKey { get; set; }
        required public string UploadUrl { get; set; }
        required public DateTimeOffset ExpiresAt { get; set; }
    }
}
