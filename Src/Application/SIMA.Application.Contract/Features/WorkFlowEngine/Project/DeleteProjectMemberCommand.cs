using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class DeleteProjectMemberCommand : ICommand<Result<long>>
    {
        public long ProjectId { get; set; }
        public long Id { get; set; }
    }
}
