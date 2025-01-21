using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories
{
    public class GetAllSenarioExecutionHistoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetSenarioExecutionHistoryQueryResult>>>
    {
    }
}
