using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Dtos
{
    public class SaveStateDownloadUrlRequest
    {
        [Required(ErrorMessage = "RomHash is required.")]
        required public string RomHash { get; set; }

        [Required(ErrorMessage = "Slot is required.")]
        required public int Slot { get; set; }
    }
}
