﻿using CloudStates.API.Dtos;

namespace CloudStates.API.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        RefreshResponse Refresh(int userId);
    }
}
