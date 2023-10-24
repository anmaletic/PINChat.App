using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using PINChat.App.Authentication;
using PINChat.App.Client;
using PINChat.App.Library.Api;
using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;
using PINChat.App.Library.Models.Interfaces;
using PINChat.App.Library.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//  Personal Services


builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

var chatServer = builder.Configuration.GetValue<string>("chatServer");

HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl(chatServer, options =>
    {
        options.Transports = HttpTransportType.WebSockets;
    })
    .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5) })
    .Build();

builder.Services.AddSingleton(hubConnection);

builder.Services.AddSingleton<IChatService, ChatService>();

builder.Services.AddSingleton<IApiHelper, ApiHelper>();
builder.Services.AddSingleton<ILoggedInUserModel, UserModel>();
builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();

builder.Services.AddHttpClient("PINChat.App.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PINChat.App.ServerAPI"));

await builder.Build().RunAsync();