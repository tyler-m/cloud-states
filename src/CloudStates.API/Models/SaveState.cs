
namespace CloudStates.API.Models
{
    public class SaveState
    {
        public int Id { get; set; }
        public string RomHash { get; set; }
        public int Slot { get; set; }
        public string FileKey { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
