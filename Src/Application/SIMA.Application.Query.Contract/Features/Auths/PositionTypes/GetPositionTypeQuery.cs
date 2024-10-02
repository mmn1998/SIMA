using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.PositionTypes;

public class GetPositionTypeQuery : IQuery<Result<GetPositionTypeQueryResult>>
{
    public long Id { get; set; }
}