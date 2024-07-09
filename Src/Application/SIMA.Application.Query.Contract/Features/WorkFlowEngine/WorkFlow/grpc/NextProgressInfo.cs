namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class NextProgressInfo
{
    public long? TargetId { get; set; }
    public string? ConditionExpression { get; set; }
    public string? Extension { get; set; }
    public long? NextStateId { get; set; }
    public string? SpName{ get; set; }

}
//public class StoreProcedureParams
//{
//    public long Id { get; set; }
//    public string Name { get; set; }
//    public bool IsRequired { get; set; }
//}


public class StoreProcedureInfo
{
    public long ProgressId { get; set; }
    public string StoreProcedureName { get; set; }
    public long ProgressStoreProcedureId { get; set; }
    public long ParamId { get; set; }
    public string ParamName { get; set; }
    public string IsSystemParam { get; set; }
    public string SystemParamName { get; set; }
    public long DataTypeId { get; set; }
    public float ExecutionOrdering { get; set; }
    public List<StoreProcedureParamInfo> Params { get; set; }

}
public class StoreProcedureParamInfo
{
    public long ParamId { get; set; }
    public long DataTypeId { get; set; }
    public string ParamName { get; set; }
    public string IsSystemParam { get; set; }
    public string SystemParamName { get; set; }
    public string Value { get; set; }
}
