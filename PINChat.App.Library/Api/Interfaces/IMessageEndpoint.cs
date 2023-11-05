using PINChat.App.Library.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IMessageEndpoint
{
    Task<List<MessageModel>> GetById(MessageQueryModel model);
    Task<string> CreateNew(MessageModel model);
}