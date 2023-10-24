namespace PINChat.App.Shared.Models;

public class MessageDtoModel
{
    public DateTime Date { get; set; }
    public string? Target { get; set; }
    public string? Source { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
}
