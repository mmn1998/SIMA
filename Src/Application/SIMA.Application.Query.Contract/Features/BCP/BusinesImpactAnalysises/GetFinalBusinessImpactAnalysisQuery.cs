using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;

public class GetFinalBusinessImpactAnalysisQuery: IQuery<Result<GetBusinessImpactAnalysisQueryResult>>
{
    public long ServiceId { get; set; }
}