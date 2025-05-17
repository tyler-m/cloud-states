using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Dtos
{
    public class SaveStateLatestRequest
    {
        [Required(ErrorMessage = "RomHash is required.")]
        required public string RomHash { get; set; }

        [Required(ErrorMessage = "Slot is required.")]
        required public int Slot { get; set; }
    }
}
