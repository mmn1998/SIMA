using SIMA.Application.Query.Contract.Features.Notifications.Messages;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Notifications.Messages
{
    public interface IMessageQueryRepository : IQueryRepository
    {
        Task<Result<IEnumerable<GetMessageQueryResult>>> GetAll(GetAllMessagesQuery request);
        Task<Result<IEnumerable<GetMessageQueryResult>>> GetAllForUser(GetAllMessageForUserQuery request);
        Task<GetMessageQueryResult> GetById(long id);
        Task<string> CheckSeenMessage(long messageId, long userId);
    }
}
