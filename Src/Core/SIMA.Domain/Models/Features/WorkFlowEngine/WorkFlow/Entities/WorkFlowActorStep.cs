using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class WorkFlowActorStep : Entity
{

    private WorkFlowActorStep()
    {

    }
    private WorkFlowActorStep(CreateWorkFlowActorStepArg arg)
    {
        Id = new WorkFlowActorStepId(arg.Id);
        StepId = new StepId(arg.StepId);
        WorkFlowActorId = new WorkFlowActorId(arg.WorkFlowActorId);
        CreatedAt = DateTime.UtcNow;
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public static WorkFlowActorStep New(CreateWorkFlowActorStepArg arg)
    {
        var result = new WorkFlowActorStep(arg);
        return result;
    }

    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }

    public WorkFlowActorStepId Id { get; set; }
    public WorkFlowActorId WorkFlowActorId { get; set; }
    public StepId StepId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public virtual Step Step { get; set; } = null!;
    public virtual SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites. WorkFlowActor WorkFlowActor { get; set; } = null!;
}
