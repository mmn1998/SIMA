using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions
{
    public class GetApprovalOptionQuery : IQuery<Result<GetApprovalOptionQueryResult>>
    {
        public long Id { get; set; }
    }
}
