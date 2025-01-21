using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetAllMessageForUserQuery : BaseRequest, IQuery<Result<IEnumerable<GetMessageQueryResult>>>
    { 

    }
}
