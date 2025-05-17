using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Dtos
{
    public class SaveStateStoreRequest
    {
        [Required(ErrorMessage = "RomHash is required.")]
        required public string RomHash { get; set; }

        [Required(ErrorMessage = "Slot is required.")]
        [Range(0, 9)]
        required public int Slot { get; set; }

        [Required(ErrorMessage = "FileKey is required.")]
        required public string FileKey { get; set; }
    }
}
