using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api;

public class MessageEndpoint : IMessageEndpoint
{
    private readonly IApiHelper _apiHelper;

    public MessageEndpoint(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }
    
    
    public async Task<List<MessageModel>> GetById(MessageQueryModel msg)
    {
            
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Message", msg);
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<MessageModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
    
    public async Task<string> CreateNew(MessageModel model)
    {
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Message/Insert", model);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
}