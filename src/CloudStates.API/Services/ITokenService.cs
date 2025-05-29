using CloudStates.API.Models;

namespace CloudStates.API.Services
{
    public interface ITokenService
    {
        TokenPair CreateTokenPair(int userId);
        Token CreateAccessToken(int userId);
    }
}
