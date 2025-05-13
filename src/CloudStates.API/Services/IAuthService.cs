using CloudStates.API.Dtos;

namespace CloudStates.API.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}
