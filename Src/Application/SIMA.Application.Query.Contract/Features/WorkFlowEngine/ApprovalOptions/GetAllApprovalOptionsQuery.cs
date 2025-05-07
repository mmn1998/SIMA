using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions
{
    public class GetAllApprovalOptionsQuery : BaseRequest, IQuery<Result<IEnumerable<GetApprovalOptionQueryResult>>>
    {
    }
}
