namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args
{
    public class ModifyBusinessContinuityPlanStratgyArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanVersioningId { get; set; }
        public long BusinessContinuityStratgyId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
