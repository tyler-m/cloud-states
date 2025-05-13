using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CloudStates.API.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            string subClaim = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                   ?? throw new UnauthorizedAccessException("Sub claim not found.");

            if (!int.TryParse(subClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid Sub claim format.");
            }

            return userId;
        }
    }
}
