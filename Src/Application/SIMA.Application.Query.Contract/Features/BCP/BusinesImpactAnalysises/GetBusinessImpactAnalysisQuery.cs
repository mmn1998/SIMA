using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;

public class GetBusinessImpactAnalysisQuery : IQuery<Result<GetBusinessImpactAnalysisQueryResult>>
{
    public long Id { get; set; }
}