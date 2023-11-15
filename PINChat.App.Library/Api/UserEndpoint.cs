using System.Net.Http.Headers;
using PINChat.App.Library.Api.Interfaces;
using PINChat.App.Library.Models;
using PINChat.App.Library.Models.Interfaces;

namespace PINChat.App.Library.Api;

public class UserEndpoint : IUserEndpoint
{
    private readonly IApiHelper _apiHelper;
    private readonly ILoggedInUserModel _loggedInUser;

    public UserEndpoint(IApiHelper apiHelper, ILoggedInUserModel loggedInUser)
    {
        _apiHelper = apiHelper;
        _loggedInUser = loggedInUser;
    }

    public async Task<AuthenticatedUserModel> Authenticate(string username, string password)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

        using var response = await _apiHelper.ApiClient.PostAsync("/Token", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<AuthenticatedUserModel>();
            return result;
        }

        throw new Exception(response.ReasonPhrase);
    }

    public void LogOffUser()
    {
        _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
    }

    public async Task GetLoggedInUserInfo(string token)
    {
        _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
        _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
        _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        using var response = await _apiHelper.ApiClient.GetAsync("/api/User");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<UserModel>();
                
            _loggedInUser.Id = result.Id;
            _loggedInUser.DisplayName = result.DisplayName;
            _loggedInUser.FirstName = result.FirstName;
            _loggedInUser.LastName = result.LastName;
            _loggedInUser.Avatar = result.Avatar;
            _loggedInUser.Contacts = result.Contacts;
            _loggedInUser.Groups = result.Groups;

            _loggedInUser.Contacts =
                _loggedInUser.Contacts.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
            _loggedInUser.Groups = _loggedInUser.Groups.OrderBy(x => x.Name).ToList();
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<List<UserModel>> GetAll()
    {
        using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/User/GetAll"))
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<List<UserModel>>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

    public async Task<string> UpdateUser(UserModel user)
    {
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Update", user);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
    
    public async Task<string> AddContact(string userId, string contactId)
    {
        var p = new
        {
            UserId = userId,
            ContactId = contactId
        };
        
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Contact/Insert", p);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
    public async Task<string> RemoveContact(string userId, string contactId)
    {
        var p = new
        {
            UserId = userId,
            ContactId = contactId
        };
        
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Contact/Delete", p);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
    
    public async Task<string> AddGroup(string userId, string groupId)
    {
        var p = new
        {
            UserId = userId,
            GroupId = groupId
        };
        
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Group/Insert", p);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
    public async Task<string> RemoveGroup(string userId, string groupId)
    {
        var p = new
        {
            UserId = userId,
            GroupId = groupId
        };
        
        using var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Group/Delete", p);
        
        var msg = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return msg;
        }

        throw new Exception(msg);
    }
}