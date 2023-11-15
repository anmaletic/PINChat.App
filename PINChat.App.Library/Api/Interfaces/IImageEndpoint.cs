using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IImageEndpoint
{
    string GetUserImage(UserModel user);
}