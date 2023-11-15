namespace PINChat.App.Library.Models.Interfaces;

public interface ILoggedInUserModel
{
    string? Id { get; set; }
    string? DisplayName { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    byte[]? Avatar { get; set; }
    string? AvatarPath { get; set; }
    public List<UserModel> Contacts { get; set; }
    public List<GroupModel> Groups { get; set; }
    string FullName { get; }
}