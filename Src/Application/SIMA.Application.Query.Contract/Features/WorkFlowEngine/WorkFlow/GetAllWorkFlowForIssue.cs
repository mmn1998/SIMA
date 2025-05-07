using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class GetAllWorkFlowForIssue : BaseRequest, IQuery<Result<IEnumerable<GetWorkFlowQueryResult>>>
    {
    }
}
