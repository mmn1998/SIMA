namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Args
{
    public class IssueRunActionArg
    {
        public long CurrentStepId { get; set; }
        public long CurrentStateId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
