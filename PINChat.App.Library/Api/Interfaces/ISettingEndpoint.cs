using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface ISettingEndpoint
{
    Task<SettingModel> GetByKey(string key);
}