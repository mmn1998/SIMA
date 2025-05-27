namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;

public class CreateDataProcedureSupportTeamCommand
{
    public long StaffId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
}
