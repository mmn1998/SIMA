using SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;

public class GetAllScenarioHistoryQuery : BaseRequest, IQuery<Result<IEnumerable<GetScenarioHistoryQueryResult>>>
{
    
}