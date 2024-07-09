namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;

public class ProgressStoreProcedureParamArg
{
    public long Id { get; set; }
    public long ProgressStoreProcedureId { get; set; }
    public string Name { get; set; }
    public string IsRequierd { get; set; }
    public string IsSystemParam { get; set; }
    public string SystemParamName { get; set; }
    public string DisplayName { get; set; }
    public long DataTypeId { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }

}
