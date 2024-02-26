using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetAllUserQuery : IQuery<Result<List<GetUserQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
