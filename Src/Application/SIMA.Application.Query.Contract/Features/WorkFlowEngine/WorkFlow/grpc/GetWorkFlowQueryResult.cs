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
    public long ActionTypeId { get; set; }
    
}
// long workflowId, long nextStepId, long progressId, string ConditionValue
public class GetNextStepQuery
{
    public long WorkflowId { get; set; }
    public long NextStepId { get; set; }
    public long ProgressId { get; set; }
    public string ConditionValue { get; set; }
    public List<InputModel> SystemParams { get; set; } = new List<InputModel>();
   
}
public class InputModel
{
    public string Key { get; set; }
    public string Value { get; set; }
}
public class InputParamQueryModel
{
    public long Id { get; set; }
    public string ParamName { get; set; }
    public string ParamValue { get; set; }
}