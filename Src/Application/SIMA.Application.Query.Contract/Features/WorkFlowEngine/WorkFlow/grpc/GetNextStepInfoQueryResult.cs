namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class GetNextStepInfoQueryResult
{
    public long? CurrentProgressStateId { get; set; }
    public long CurrentProgressId { get; set; }
    public long StepId { get; set; }
    public long WorkflowId { get; set; }
    public long ActionTypeId { get; set; }
    public string? SpName { get; set; }
    public string? URLAddress { get; set; }
    public long? ApiMethodActionId { get; set; }
    public string? APIMethodName { get; set; }
    public string? InputServiceName { get; set; }
    public long? DataTypeId { get; set; }
    public string? DataTypeName { get; set; }
    public List<NextProgressInfo> NextProgressInfo { get; set; }
    //public List<StoreProcedureParams> Params { get; set; }

}

public class GetProgressModel
{
    public long StateId { get; set; }
    public long ProgressId { get; set; }
}
public class GetNextStateModel
{
    public long Id { get; set; }
    public long WorkflowId { get; set; }
    public long ActionTypeId { get; set; }
}