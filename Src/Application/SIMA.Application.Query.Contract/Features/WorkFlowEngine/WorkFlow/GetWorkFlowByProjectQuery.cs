using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public  class GetWorkFlowByProjectQuery : IQuery<Result<List<GetWorkFlowQueryResult>>>
    {
        public long ProjectId { get; set; }
    }
}
