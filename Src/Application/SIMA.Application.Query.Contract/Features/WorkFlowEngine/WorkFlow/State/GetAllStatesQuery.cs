using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class GetAllStatesQuery :BaseRequest, IQuery<Result<IEnumerable<GetStateQueryResult>>>
    {
        public long? WorkFlowId { get; set; }
        public long? ProjectId { get; set; }
        public long? DomainId { get; set; }
    }
}
