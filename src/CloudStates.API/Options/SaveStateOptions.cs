using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Options
{
    internal class SaveStateOptions
    {
        [Required]
        required public string FileExtension { get; set; }

        [Required]
        [Range(1, 15)]
        required public double MinutesUntilUploadUrlExpires { get; set; }
    }
}
