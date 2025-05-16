
namespace CloudStates.API.Dtos
{
    public class SaveStateStoreResponse
    {
        required public string RomHash { get; set; }
        required public int Slot { get; set; }
    }
}
