using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class CreateProjectMemberCommand : ICommand<Result<long>>
    {
        public List<ProjectMemberListCommand> ProjectMember { get; set; }
        public long ProjectId { get; set; }
    }
}
