using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class GetStatesByWorkFlowQuery : IQuery<Result<List<GetStateQueryResult>>>
    {
        public long Id { get; set; }
    }
}
