using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Options
{
    internal class JwtOptions
    {
        [Required]
        public required string Secret { get; set; }

        [Required]
        public required string Issuer { get; set; }

        [Required]
        public required string AccessAudience { get; set; }

        [Required]
        public required string RefreshAudience { get; set; }

        [Required]
        [Range(1, 120)]
        public double MinutesUntilAccessExpiration { get; set; }

        [Required]
        [Range(1440, 10080)]
        public double MinutesUntilRefreshExpiration { get; set; }
    }
}
