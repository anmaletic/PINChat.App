using PINChat.App.Library.Models.Interfaces;

namespace PINChat.App.Library.Models;

public class UserModel : ILoggedInUserModel
{
    
    public string? Id { get; set; }
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    // public List<InterestModel> Interests { get; set; }
    
    public void ResetUserModel()
    {
        Id = "";
        DisplayName = "";
        FirstName = "";
        LastName = "";
        // Interests = new List<InterestModel>();
    }
}