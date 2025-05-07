using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;

public class GetCobitScenariosByCategoryQuery : IQuery<Result<IEnumerable<GetCobitScenarioQueryResult>>>
{
    public long CategoryId { get; set; }
}