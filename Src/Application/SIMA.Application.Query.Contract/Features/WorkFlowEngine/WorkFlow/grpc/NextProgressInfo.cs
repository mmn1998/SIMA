namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class NextProgressInfo
{
    public long? TargetId { get; set; }
    public string? ConditionExpression { get; set; }
    public string? Extension { get; set; }
    public long? NextStateId { get; set; }

}
