namespace PINChat.App.Library.Models.Interfaces;

public interface ILoggedInUserModel
{
    string? Id { get; set; }
    string? DisplayName { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    void ResetUserModel();
}