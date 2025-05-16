
namespace CloudStates.API.Models
{
    public class TokenPair
    {
        required public Token AccessToken { get; set; }
        required public Token RefreshToken { get; set; }
    }
}
