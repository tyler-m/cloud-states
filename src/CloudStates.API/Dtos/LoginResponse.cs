
namespace CloudStates.API.Dtos
{
    public class LoginResponse
    {
        required public string AccessToken { get; set; }
        required public DateTimeOffset AccessExpiresAt { get; set; }
        required public string RefreshToken { get; set; }
        required public DateTimeOffset RefreshExpiresAt { get; set; }
    }
}
