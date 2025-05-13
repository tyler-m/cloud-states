using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CloudStates.API.Models;
using CloudStates.API.Options;

namespace CloudStates.API.Services
{
    internal class TokenService(JwtOptions _options) : ITokenService
    {
        public TokenPair CreateTokenPair(User user)
        {
            return new TokenPair
            {
                AccessToken = CreateAccessToken(user),
                RefreshToken = CreateRefreshToken(user)
            };
        }

        public Token CreateAccessToken(User user)
        {
            return CreateToken(user, _options.MinutesUntilAccessExpiration, _options.AccessAudience);
        }

        private Token CreateRefreshToken(User user)
        {
            return CreateToken(user, _options.MinutesUntilRefreshExpiration, _options.RefreshAudience);
        }

        private Token CreateToken(User user, double minutesUntilExpiration, string audience)
        {
            string? secret = _options.Secret;

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secret));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            DateTime expiresAt = DateTime.UtcNow.AddMinutes(minutesUntilExpiration);

            JwtSecurityToken accessToken = new(
                issuer: _options.Issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new Token
            {
                TokenString = new JwtSecurityTokenHandler().WriteToken(accessToken),
                ExpiresAt = expiresAt
            };
        }
    }
}
