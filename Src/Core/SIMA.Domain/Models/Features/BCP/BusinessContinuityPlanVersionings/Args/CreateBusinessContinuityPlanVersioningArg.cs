using System;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Args
{
    public class CreateBusinessContinuityPlanVersioningArg
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanId { get; set; }
        public string VersionNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
