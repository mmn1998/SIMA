using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.PositionLevels;

public class GetPositionLevelQuery : IQuery<Result<GetPositionLevelQueryResult>>
{
    public long Id { get; set; }
}