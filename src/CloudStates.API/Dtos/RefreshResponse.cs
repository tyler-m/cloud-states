namespace CloudStates.API.Dtos
{
    public class RefreshResponse
    {
        required public string AccessToken { get; set; }
        required public DateTimeOffset ExpiresAt { get; set; }
    }
}
