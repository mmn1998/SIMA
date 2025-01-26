using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;

public class GetScenarioHistoryQuery: IQuery<Result<GetScenarioHistoryQueryResult>>
{
    public long Id { get; set; }
}