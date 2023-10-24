using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using PINChat.App.Library.Api.Interfaces;

namespace PINChat.App.Library.Api;

public class ApiHelper : IApiHelper
{
    private readonly IConfiguration _config;
    private HttpClient? _apiClient;


    public ApiHelper(IConfiguration config)
    {
        _config = config;

        InitializeClient();
    }

    public HttpClient ApiClient => _apiClient!;

    private void InitializeClient()
    {
        var api = _config.GetValue<string>("api");

        _apiClient = new HttpClient
        {
            BaseAddress = new Uri(api)
        };
        _apiClient.DefaultRequestHeaders.Accept.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}