namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args
{
    public class ModifyBusinessContinuityStratgySolutionArg
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public long BusinessContinuityStratgyId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
