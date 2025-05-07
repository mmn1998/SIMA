using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories
{
    public class GetSenarioExecutionHistoryQuery : IQuery<Result<GetSenarioExecutionHistoryQueryResult>>
    {
        public long Id { get; set; }
    }
}
