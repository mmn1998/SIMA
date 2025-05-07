using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step
{
    public class GetStepsByWorkFlowQuery : IQuery<Result<List<GetStepQueryResult>>>
    {
        public long Id { get; set; }
    }
}
