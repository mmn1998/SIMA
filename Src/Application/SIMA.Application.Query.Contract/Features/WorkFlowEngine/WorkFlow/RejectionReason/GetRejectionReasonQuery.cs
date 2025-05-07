using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.RejectionReason
{
    public class GetRejectionReasonQuery : IQuery<Result<GetRejectionReasonQueryResult>>
    {
        public int Id { get; set; }
    }
}
