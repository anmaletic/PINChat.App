using PINChat.App.Library.Models.Interfaces;

namespace PINChat.App.Library.Models;

public class UserModel : TargetModel, ILoggedInUserModel
{
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<UserModel> Contacts { get; set; } = new ();
    public List<GroupModel> Groups { get; set; } = new ();
    
    public void ResetUserModel()
    {
        Id = "";
        DisplayName = "";
        FirstName = "";
        LastName = "";
        Contacts = new List<UserModel>();
        Groups = new List<GroupModel>();
    }
    
    public string FullName => $"{FirstName} {LastName}";
}