namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

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
    public string? FixedValue { get; set; }
    public string? JsonFormat { get; set; }

    public List<StoreProcedureParamInfo> Params { get; set; }

}
