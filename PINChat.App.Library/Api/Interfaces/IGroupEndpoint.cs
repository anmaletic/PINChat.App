using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IGroupEndpoint
{
    Task<List<GroupModel>> GetAll();
}