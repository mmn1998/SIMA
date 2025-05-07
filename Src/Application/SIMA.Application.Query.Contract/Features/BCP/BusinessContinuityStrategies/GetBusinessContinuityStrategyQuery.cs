using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;

public class GetBusinessContinuityStrategyQuery : IQuery<Result<GetBusinessContinuityStrategyQueryResult>>
{
    public long Id { get; set; }
}