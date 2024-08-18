namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args
{
    public class ModifyBusinessContinuityPlanResponsibleArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanVersioningId { get; set; }
        public long StaffId { get; set; }
        public long PlanResponsibilityId { get; set; }
        public string IsForBackup { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
