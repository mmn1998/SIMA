namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;

public class GetProgressQueryResult
{
    public long Id { get; set; }
    public long? StateId { get; set; }
    public string? Name { get; set; }
    public string? StatusName { get; set; }
    public int? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public string? WorkFlowName { get; set; }
    public long? WorkFlowId { get; set; }
    public string? DomainName { get; set; }
    public long? DomainID { get; set; }
    public string? ProjectName { get; set; }
    public long? ProjectId { get; set; }
    public string? SourceName { get; set; }
    public long? SourceId { get; set; }
    public string? TargetName { get; set; }
    public long? TargetNameId { get; set; }
    public string? ConditionExpression { get; set; }
    public IEnumerable<GetProgressStoreProcedureQueryResult>? StoreProcedures { get; set; }
}
public class GetProgressStoreProcedureQueryResult
{
    public long Id { get; set; }
    public string? StoreProcedureName { get; set; }
    public int ExecutionOrdering { get; set; }
    public IEnumerable<GetProgressStoreProcedureParamQueryResult>? Params { get; set; }
}
public class GetProgressStoreProcedureParamQueryResult
{
    public long ProcedureId { get; set; }
    public long Id { get; set; }
    public string? Name { get; set; }
    public long DataTypeId { get; set; }
    public string? IsRequired { get; set; }
    public string? IsSystemParam { get; set; }
    public string? SystemParamName { get; set; }
    public string? DisplayName { get; set; }
    public string? JsonFormat { get; set; }
    public string? BoundFormat { get; set; }
    public string? ApiNameForDataBounding { get; set; }
    public string? StoredProcedureForDataBounding { get; set; }
    public long? UiInputElementId { get; set; }
}
