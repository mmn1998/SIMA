using SIMA.Application.Query.Contract.Features.Notifications.Messages;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Notifications.Messages;

namespace SIMA.Application.Query.Features.Notifications.Messages
{
    public class MessageQueryHandler : IQueryHandler<GetMessageQuery, Result<GetMessageQueryResult>>,
    IQueryHandler<GetAllMessagesQuery, Result<IEnumerable<GetMessageQueryResult>>>,
    IQueryHandler<GetAllMessageForUserQuery, Result<IEnumerable<GetMessageQueryResult>>>,
    IQueryHandler<GetCountSeenNotificationQuery, Result<GetCountSeenNotificationQueryResult>>

    {
        private readonly IMessageQueryRepository _repository;
        private readonly ISimaIdentity _simaIdentity;

        public MessageQueryHandler(IMessageQueryRepository repository , ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _simaIdentity = simaIdentity;   
        }
        public async Task<Result<GetMessageQueryResult>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetMessageQueryResult>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<IEnumerable<GetMessageQueryResult>>> Handle(GetAllMessageForUserQuery request, CancellationToken cancellationToken)
        {
            var messages = await _repository.GetAllForUser(request);
            foreach (var item in messages.Data)
            {
                item.IsSeen = await _repository.CheckSeenMessage(item.Id, _simaIdentity.UserId);
            }
            return messages;
        }

        public async Task<Result<GetCountSeenNotificationQueryResult>> Handle(GetCountSeenNotificationQuery request, CancellationToken cancellationToken)
        {
            var req = new GetAllMessageForUserQuery();
            var result = new GetCountSeenNotificationQueryResult();
            var messages = await _repository.GetAllForUser(req);
            foreach (var item in messages.Data)
            {
                item.IsSeen = await _repository.CheckSeenMessage(item.Id, _simaIdentity.UserId);
            }
            var notSeen = messages.Data.Where(x => x.IsSeen != "1").Count();
            result.SeenNumber = notSeen;
            return result;
        }
    }
}
