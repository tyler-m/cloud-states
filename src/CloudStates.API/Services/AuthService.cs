using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using CloudStates.API.Dtos;
using CloudStates.API.Models;
using CloudStates.API.Repositories;

namespace CloudStates.API.Services
{
    internal class AuthService(IUserRepository _users) : IAuthService
    {
        private const int SaltByteLength = 32;

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _users.UserWithUsernameExists(request.Username))
            {
                throw new ValidationException($"Username {request.Username} is already taken.");
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
                throw new Exception("Unable to register.");
            }

            return new RegisterResponse()
            {
                Username = user.Username
            };
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
