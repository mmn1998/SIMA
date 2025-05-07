namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;

public class ProgressStoreProcedureParamArg
{
    public long Id { get; set; }
    public long ProgressStoreProcedureId { get; set; }
    public string Name { get; set; }
    public string IsRequired { get; set; }
    public string IsSystemParam { get; set; }
    public string SystemParamName { get; set; }
    public string DisplayName { get; set; }
    public string JsonFormat { get; set; }
    public long DataTypeId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public string? BoundFormat { get; set; }
    public string? ApiNameForDataBounding { get; set; }
    public string? StoredProcedureForDataBounding { get; set; }
    public long? UiInputElementId { get; set; }
    public long? ApiMethodActionId { get; set; }

}
