using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project
{
    public class GetProjectQuery : IQuery<Result<GetProjectQueryResult>>
    {
        public long Id { get; set; }
    }
}
