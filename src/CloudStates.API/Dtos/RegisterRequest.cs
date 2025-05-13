namespace CloudStates.API.Dtos
{
    public class RegisterRequest
    {
        required public string Username { get; set; }
        required public string Password { get; set; }
    }
}
