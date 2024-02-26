namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class GetWorkflowInfoByIdResponseQueryResult
{
    public long Id { get; set; }
    public long SourceStepId { get; set; }
    public long SourceStateId { get; set; }
    public long TargetStepId { get; set; }
    public long TargetStateId { get; set; }
    public long ProjectId { get; set; }
}

