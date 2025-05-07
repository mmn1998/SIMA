using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;

public class GetStrategyTypeQuery : IQuery<Result<GetStrategyTypeQueryResult>>
{
    public long Id { get; set; }
}