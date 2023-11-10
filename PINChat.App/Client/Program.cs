using Blazor.SubtleCrypto;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PINChat.App.Authentication;
using PINChat.App.Client;
using PINChat.App.Library.Api;
using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;
using PINChat.App.Library.Models.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional:true, reloadOnChange:true);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional:true, reloadOnChange:true);
builder.Configuration.AddEnvironmentVariables();

//  Personal Services

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddSubtleCrypto(opt =>
    opt.Key = builder.Configuration.GetValue<string>("Encryption:Key"));

builder.Services.AddSingleton<IApiHelper, ApiHelper>();
builder.Services.AddSingleton<ILoggedInUserModel, UserModel>();
builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();
builder.Services.AddTransient<IGroupEndpoint, GroupEndpoint>();
builder.Services.AddTransient<IRegistrationEndpoint, RegistrationEndpoint>();
builder.Services.AddTransient<IMessageEndpoint, MessageEndpoint>();

builder.Services.AddHttpClient("PINChat.App.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PINChat.App.ServerAPI"));

await builder.Build().RunAsync();