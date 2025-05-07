namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemSupportTeamCommand
{
    public long MainStaffId { get; set; }
    public long? MainDepartmentId { get; set; }
    public long? MainBranchId { get; set; }
    public long SubsitutedStaffId { get; set; }
    public long? SubsitutedDepartmentId { get; set; }
    public long? SubsitutedBranchId { get; set; }
}
