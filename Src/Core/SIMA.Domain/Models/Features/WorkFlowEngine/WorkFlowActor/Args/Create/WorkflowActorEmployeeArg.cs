namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;

public class WorkflowActorEmployeeArg 
{
    public long EmployeeId { get; set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }

}
