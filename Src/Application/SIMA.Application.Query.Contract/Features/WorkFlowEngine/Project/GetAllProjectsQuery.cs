using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project
{
    public class GetAllProjectsQuery : BaseRequest ,  IQuery<Result<IEnumerable<GetProjectQueryResult>>>
    {
        public long? DomainId { get; set; }

    }
}
