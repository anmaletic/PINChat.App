using PINChat.App.Library.Models;
using PINChat.App.Shared.Models;

namespace PINChat.App.Library.Api.Interfaces;

public interface IMessageEndpoint
{
    Task<List<MessageModel>> GetByUserId(MessageQueryModel model);
    Task<List<MessageModel>> GetByGroupId(MessageQueryModel model);
    Task<string> CreateNew(MessageDtoModel model);
}