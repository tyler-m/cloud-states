using System.ComponentModel.DataAnnotations;

namespace CloudStates.API.Options
{
    public class CleanupOptions
    {
        [Required]
        [Range(1, 120)]
        public double MinutesBetweenCleanup { get; set; }
    }
}
