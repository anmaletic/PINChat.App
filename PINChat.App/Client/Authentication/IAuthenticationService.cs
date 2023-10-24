using PINChat.App.Library.Models;

namespace PINChat.App.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
    Task Logout();
}