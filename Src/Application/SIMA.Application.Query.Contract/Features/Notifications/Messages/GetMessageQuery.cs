using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetMessageQuery : IQuery<Result<GetMessageQueryResult>>
    {
        public long Id { get; set; }
    }
}
