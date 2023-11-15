using Microsoft.Extensions.Configuration;
using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api;

public class ImageEndpoint : IImageEndpoint
{
    private readonly IApiHelper _apiHelper;
    private readonly IConfiguration _config;

    public ImageEndpoint(IApiHelper apiHelper, IConfiguration config)
    {
        _apiHelper = apiHelper;
        _config = config;
    }

    public string GetUserImage(UserModel user)
    {
        var api = _config.GetValue<string>("api");
        
        return $"{api}/api/Image/GetImage/{user.Id}?t={DateTime.Now.Ticks}";
    }
}