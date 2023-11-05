using PINChat.App.Library.Models.Interfaces;

namespace PINChat.App.Library.Models;

public class MessageModel
{
    private ILoggedInUserModel _loggedInUser;
        
    public MessageModel(ILoggedInUserModel loggedInUser)
    {
        _loggedInUser = loggedInUser;
    }

    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? TargetId { get; set; }
    public string? SourceId { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
    public bool IsOrigin { get; set; }
}