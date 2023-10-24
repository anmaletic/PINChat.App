using PINChat.App.Library.Models;
using PINChat.App.Shared.Models;

namespace PINChat.App.Library.Services;

public interface IChatService
{
    Task<bool> Connect();
    Task<bool> Login(string userName);
    Task MessageSingle(string action, MessageModel message);
    event Action<string, MessageDtoModel> MessageEvent;
}