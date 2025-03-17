namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemSupportTeamArg
{
    public long Id { get; set; }
    public long ConfigurationItemId { get; set; }
    public long MainStaffId { get; set; }
    public long? MainDepartmentId { get; set; }
    public long? MainBranchId { get; set; }
    public long SubsitutedStaffId { get; set; }
    public long? SubsitutedDepartmentId { get; set; }
    public long? SubsitutedBranchId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}