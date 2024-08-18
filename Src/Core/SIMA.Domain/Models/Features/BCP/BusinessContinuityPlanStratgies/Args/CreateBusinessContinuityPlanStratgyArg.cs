namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args
{
    public class CreateBusinessContinuityPlanStratgyArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanVersioningId { get; set; }
        public long BusinessContinuityStratgyId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
