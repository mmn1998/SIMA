using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create
{
    public class CreateWorkFlowActorRoleArg
    {
        public WorkFlowActorId WorkFlowActorId { get; set; }
        public long RoleId { get; set; }
        public long ActiveStatusId { get; set; }
    }
}
