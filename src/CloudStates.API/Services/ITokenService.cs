using CloudStates.API.Models;

namespace CloudStates.API.Services
{
    public interface ITokenService
    {
        TokenPair CreateTokenPair(User user);
        Token CreateAccessToken(User user);
    }
}
