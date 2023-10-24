using PINChat.App.Library.Models.Interfaces;

namespace PINChat.App.Library.Models;

public class MessageModel
{
    private ILoggedInUserModel _loggedInUser;
        
    public MessageModel(ILoggedInUserModel loggedInUser)
    {
        _loggedInUser = loggedInUser;
    }

    public DateTime Date { get; set; }
    public string? Target { get; set; }
    public string? Source { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }

    public bool IsOrigin => _loggedInUser.Id == Source;
}