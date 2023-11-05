using Microsoft.AspNetCore.SignalR;
using PINChat.App.Shared.Models;

namespace PINChat.App.Server;

public class ChatHub : Hub
{
    public class User
    {
        public string? Name { get; set; }
        public string? ConnectionId { get; set; }
    }

    private static List<User> _users = new List<User>();

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"<> User connected");

        return base.OnConnectedAsync();
    }
        
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Remove the disconnected user from the list
        var user = _users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
        if (user != null)
        {
            _users.Remove(user);
            Console.WriteLine($"User {user.Name} disconnected");
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async void Login(string username)
    {
        var existingUser = _users.FirstOrDefault(x => x.Name == username);

        if (existingUser is not null){
                
            _users.Remove(existingUser);
        }
        _users.Add(new User()
        {
            Name = username,
            ConnectionId = Context.ConnectionId
        });
        Console.WriteLine($"<> User name: {username}");
        Console.WriteLine($"<> Connection ID: {Context.ConnectionId}");
        await Clients.All.SendAsync("ReceiveMessage", "Server", "message");
    }

    public async Task MessageSingle(string action, MessageDtoModel messageDto)
    {
        try
        {
            var recipient = _users.FirstOrDefault((user) => user.Name == messageDto.TargetId);
                
            if (recipient is not null) 
            {
                Console.WriteLine($"<> Message received:");
                Console.WriteLine($"<> Recipient name: {recipient.Name}");
                Console.WriteLine($"<> Recipient ID: {recipient.ConnectionId}");
                Console.WriteLine($"<> Date: {messageDto.CreatedDate}");
                Console.WriteLine($"<> Source: {messageDto.SourceId}");
                Console.WriteLine($"<> Message: {messageDto.Content}");
                
                await Clients.Client(recipient.ConnectionId!).SendAsync("ReceiveMessage", action, messageDto);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}