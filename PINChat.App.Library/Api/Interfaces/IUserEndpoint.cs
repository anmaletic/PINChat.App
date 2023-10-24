using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IUserEndpoint
{
    Task<AuthenticatedUserModel> Authenticate(string username, string password);
    void LogOffUser();
    Task GetLoggedInUserInfo(string token);
    Task<List<UserModel>> GetAll();
}