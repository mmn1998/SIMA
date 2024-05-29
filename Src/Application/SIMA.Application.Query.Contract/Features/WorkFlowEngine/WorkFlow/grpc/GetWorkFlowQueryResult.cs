namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class GetWorkflowInfoByIdResponseQueryResult
{
    public long Id { get; set; }
    public long SourceStepId { get; set; }
    public long? SourceStateId { get; set; }
    public long TargetStepId { get; set; }
    public long TargetStateId { get; set; }
    public long ProjectId { get; set; }
    public long MainAggregateId { get; set; }
}
// long workflowId, long nextStepId, long progressId, string ConditionValue
public class GetNextStepQuery
{
    public long WorkflowId { get; set; }
    public long NextStepId { get; set; }
    public long ProgressId { get; set; }
    public string ConditionValue { get; set; }
    public List<FormModel> Form { get; set; }
}
public class FormModel
{
    public string Key { get; set; }
    public string Value { get; set; }
}