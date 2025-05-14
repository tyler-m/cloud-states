using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Options
{
    internal class S3Options
    {
        [Required]
        public required string AccessKey { get; set; }

        [Required]
        public required string SecretKey { get; set; }

        public string? ServiceUrl { get; set; }

        [Required]
        public required string SaveStatesBucket { get; set; }

        [Required]
        public required int Protocol { get; set; }
    }
}
