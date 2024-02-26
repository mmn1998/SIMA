using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetAllGroupQuery : IQuery<Result<List<GetGroupQueryResult>>>
{
    public BaseRequest Request { get; set; }
}

