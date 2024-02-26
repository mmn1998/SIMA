using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project
{
    public class GetProjectsByDomainQuery : IQuery<Result<List<GetProjectQueryResult>>>
    {
        public long DomainId { get; set; }
    }
}
