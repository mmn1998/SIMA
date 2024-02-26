using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class CreateWorkFlowActorRoleCommand : ICommand<Result<long>>
    {
        public long WorkFlowActorId { get; set; }
        public List<long> RoleId { get; set; }
    }
}
