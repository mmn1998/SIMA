namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;

public class CreateDataProcedureSupportTeamArg
{
    public long Id { get; set; }
    public long DataProcedureId { get; set; }
    public long StaffId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
