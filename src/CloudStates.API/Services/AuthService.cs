using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using CloudStates.API.Dtos;
using CloudStates.API.Exceptions;
using CloudStates.API.Models;
using CloudStates.API.Repositories;

namespace CloudStates.API.Services
{
    internal class AuthService(
        IUserRepository _users,
        ITokenService _tokenService) : IAuthService
    {
        private const int SaltByteLength = 32;

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _users.GetByUsernameAsync(request.Username) != null)
            {
                throw new ValidationException($"Username '{request.Username}' is already taken.");
            }

            byte[] passwordSalt = GenerateSalt();
            byte[] passwordHash = HashPassword(request.Password, passwordSalt);

            User user = new()
            {
                Username = request.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            if (await _users.AddAsync(user) == null)
            {
                throw new PersistenceException("Unable to register.");
            }

            return new RegisterResponse()
            {
                Username = user.Username
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            User? user = await _users.GetByUsernameAsync(request.Username)
                ?? throw new ValidationException($"Unable to find user '{request.Username}'.");

            if (!VerifyUser(user, request.Password))
            {
                throw new ValidationException("Invalid credentials.");
            }

            TokenPair tokenPair = _tokenService.CreateTokenPair(user.Id);

            return new LoginResponse()
            {
                AccessToken = tokenPair.AccessToken.TokenString,
                AccessExpiresAt = tokenPair.AccessToken.ExpiresAt,
                RefreshToken = tokenPair.RefreshToken.TokenString,
                RefreshExpiresAt = tokenPair.RefreshToken.ExpiresAt
            };
        }

        public RefreshResponse Refresh(int userId)
        {
            Token token = _tokenService.CreateAccessToken(userId);

            return new RefreshResponse()
            {
                AccessToken = token.TokenString,
                ExpiresAt = token.ExpiresAt
            };
        }

        private static bool VerifyUser(User user, string password)
        {
            using HMACSHA512 hmac = new(user.PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(user.PasswordHash);
        }

        private static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(SaltByteLength);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            using HMACSHA512 hmac = new(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
