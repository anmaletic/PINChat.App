using Microsoft.AspNetCore.SignalR.Client;
using PINChat.App.Library.Models;
using PINChat.App.Shared.Models;

namespace PINChat.App.Library.Services;

public class ChatService : IChatService
{
    private readonly HubConnection _hubConnection;
    
    public event Action<string, MessageDtoModel> MessageEvent;
    
    public ChatService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    
        _hubConnection.On<string, MessageDtoModel>("ReceiveMessage", (action, message) => MessageEvent?.Invoke(action, message));
    }
    
    public async Task<bool> Connect()
    {
        try
        {
            await _hubConnection.StartAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<bool> Login(string userName)
    {
        try
        {
            await _hubConnection.InvokeAsync("Login", userName);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task MessageSingle(string action, MessageModel message)
    {
        
        await _hubConnection.SendAsync("MessageSingle", action, message);
    }
    
    
    // /// <summary>
    // /// Reconnect to CharServer
    // /// </summary>
    // // hubConnection.Closed += async (exception) =>
    // // {
    // //     Console.WriteLine("Connection closed. Attempting to reconnect...");
    // // };
    // //
    // // hubConnection.Reconnecting += (exception) =>
    // // {
    // //     Console.WriteLine("Reconnecting...");
    // //     return Task.CompletedTask;
    // // };
    // //
    // // hubConnection.Reconnected += (connectionId) =>
    // // {
    // //     Console.WriteLine($"Reconnected. New connection ID: {connectionId}");
    // // };
}
