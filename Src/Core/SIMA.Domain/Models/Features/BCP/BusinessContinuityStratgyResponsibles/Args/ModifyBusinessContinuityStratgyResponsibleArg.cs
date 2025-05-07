namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args
{
    public class ModifyBusinessContinuityStratgyResponsibleArg
    {
        public long Id { get; set; }
        public long BusinessContinuityStrategyId { get; set; }
        public long StaffId { get; set; }
        public long PlanResponsibilityId { get; set; }
        public string IsForBackup { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
