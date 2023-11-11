using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api;

public class SettingEndpoint : ISettingEndpoint
{
    private readonly IApiHelper _apiHelper;

    public SettingEndpoint(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }
    
    
    public async Task<SettingModel> GetByKey(string key)
    {
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Setting/GetByKey", new SettingModel() { Key = key});
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<SettingModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}