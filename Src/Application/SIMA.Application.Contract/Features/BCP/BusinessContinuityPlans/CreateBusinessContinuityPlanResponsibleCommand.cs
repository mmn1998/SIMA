namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans
{
    public class CreateBusinessContinuityPlanResponsibleCommand
    {
        public long StaffId { get; set; }
        public long PlanResponsibilityId { get; set; }
        public string IsForBackup { get; set; }
    }
}
