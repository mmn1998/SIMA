namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args
{
    public class CreateBusinessContinuityStratgyResponsibleArg
    {
        public long Id { get; set; }
        public long BusinessContinuityStrategyId { get; set; }
        public long StaffId { get; set; }
        public long PlanResponsibilityId { get; set; }
        public string IsForBackup { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
