using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class GetWorkFlowQuery : IQuery<Result<GetWorkFlowQueryResult>>
    {
        public long Id { get; set; }
    }
}
