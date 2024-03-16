using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany
{
    public class GetAllWorkFlowCompanyQuery : BaseRequest, IQuery<Result<List<GetWorkFlowCompanyQueryResult>>>
    {
    }
}
