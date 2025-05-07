using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;

public class WorkFlowActorEmployee : Entity
{
    public WorkFlowActorId ActorId { get; set; }
    public WorkFlowActorId EmployeeId { get; set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }

    public WorkFlowActor Actor { get; private set; }
    private WorkFlowActorEmployee()
    {
    }
    private WorkFlowActorEmployee(WorkflowActorEmployeeArg arg, long createdBy)
    {
        EmployeeId = new WorkFlowActorId(arg.EmployeeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = createdBy;
    }
    public static WorkFlowActorEmployee Create(WorkflowActorEmployeeArg arg, long createdBy)
    {
        return new WorkFlowActorEmployee(arg, createdBy);
    }
    public void Activate()
    {
        ActiveStatusId = (long) ActiveStatusEnum.Active;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}