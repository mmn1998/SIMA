namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class NextProgressInfo
{
    public long? TargetId { get; set; }
    public string? ConditionExpression { get; set; }
    public string? Extension { get; set; }
    public long? NextStateId { get; set; }
    public string? SpName{ get; set; }
    public long ProgressId { get; set; }
    public long WorkflowId { get; set; }
    public string? URLAddress { get; set; }
    public long? ApiMethodActionId { get; set; }
    public string? APIMethodName { get; set; }
    public string? InputServiceName { get; set; }
    public long? DataTypeId { get; set; }
    public string? DataTypeName { get; set; }

}
public class StoreProcedureParamInfo
{
    public long ParamId { get; set; }
    public long DataTypeId { get; set; }
    public string ParamName { get; set; }
    public string IsSystemParam { get; set; }
    public string SystemParamName { get; set; }
    public string Value { get; set; }
    public string? FixedValue { get; set; }
    public string? JsonFormat { get; set; }
}
