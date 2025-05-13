namespace CloudStates.API.Models
{
    public class Token
    {
        required public string TokenString { get; set; }
        required public DateTimeOffset ExpiresAt { get; set; }
    }
}
