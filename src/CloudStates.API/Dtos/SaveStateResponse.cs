
namespace CloudStates.API.Dtos
{
    public class SaveStateResponse
    {
        public int Id { get; set; }
        public string RomHash { get; set; }
        public int Slot { get; set; }
        public string FileKey { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
