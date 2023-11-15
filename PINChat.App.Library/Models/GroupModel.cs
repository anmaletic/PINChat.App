namespace PINChat.App.Library.Models;

public class GroupModel : TargetModel
{
    public string? Name { get; set; }
    public List<UserModel> Contacts { get; set; } = new ();
}