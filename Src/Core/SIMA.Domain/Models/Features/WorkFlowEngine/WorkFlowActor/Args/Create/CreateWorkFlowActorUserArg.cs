using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;

public class CreateWorkFlowActorUserArg
{
    public WorkFlowActorId WorkFlowActorId { get; set; }
    public long UserId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
