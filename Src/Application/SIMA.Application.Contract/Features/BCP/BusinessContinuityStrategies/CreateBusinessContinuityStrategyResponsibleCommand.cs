namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;

public class CreateBusinessContinuityStrategyResponsibleCommand
{
    public long StaffId { get; set; }
    public long PlanResponsibilityId { get; set; }
    public string? IsForBackup { get; set; }
}