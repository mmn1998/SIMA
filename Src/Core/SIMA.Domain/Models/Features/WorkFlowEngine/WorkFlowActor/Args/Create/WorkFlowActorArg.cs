namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;

public class WorkFlowActorArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long WorkFlowId { get; set; }
    public string BpmnId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long UserId { get; set; }
    public long? LastId { get; set; }

}
