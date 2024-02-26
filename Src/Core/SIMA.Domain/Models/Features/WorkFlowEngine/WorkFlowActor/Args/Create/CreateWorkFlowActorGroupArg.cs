using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create
{
    public class CreateWorkFlowActorGroupArg
    {
        public WorkFlowActorId WorkFlowActorId { get; set; }
        public long GroupId { get; set; }
        public long ACtiveStatusId { get; set; }
    }
}
