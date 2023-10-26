using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IRegistrationEndpoint
{
    Task<string> Register(RegistrationModel model);
}