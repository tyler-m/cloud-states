
namespace CloudStates.API.Models
{
    public class PreSignedUrl
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
