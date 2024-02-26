using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupAggregate : IQuery<Result<GetGroupAggregateResult>>
{
    public long GroupId { get; set; }
}
