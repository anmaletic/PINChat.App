using Microsoft.AspNetCore.SignalR;
using PINChat.App.Shared.Models;

namespace PINChat.App.Server;

public class ChatHub : Hub
{
    private class User
    {
        public string? UserId { get; set; }
        public string? ConnectionId { get; set; }
    }
    
    private static readonly List<User> _users = new();

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"<> User connected");

        return base.OnConnectedAsync();
    }
        
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
        if (user != null)
        {
            _users.Remove(user);
            Console.WriteLine($"User {user.UserId} disconnected");
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async void Login(string userId, List<string> groups)
    {
        try
        {
            var existingUser = _users.FirstOrDefault(x => x.UserId == userId);
            if (existingUser is not null)
            {
                _users.Remove(existingUser);
            }
        
            _users.Add(new User()
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });
        
            foreach (var group in groups)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task MessageSingle(string action, MessageDtoModel messageDto)
    {
        try
        {
            var recipient = _users.FirstOrDefault(user => user.UserId == messageDto.TargetId);
                
            if (recipient is not null) 
            {
                await Clients.Client(recipient.ConnectionId!).SendAsync("ReceiveMessage", action, messageDto);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task MessageGroup(string action, MessageDtoModel messageDto)
    {
        try
        {
                await Clients.OthersInGroup(messageDto.TargetId!).SendAsync("ReceiveMessage", action, messageDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}