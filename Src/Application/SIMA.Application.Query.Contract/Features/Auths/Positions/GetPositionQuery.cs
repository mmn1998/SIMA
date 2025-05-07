using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Positions;

public class GetPositionQuery : IQuery<Result<GetPositionQueryResult>>
{
    public long Id { get; set; }
}
