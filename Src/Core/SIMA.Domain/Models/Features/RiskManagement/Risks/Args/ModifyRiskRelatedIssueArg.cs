namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class ModifyRiskRelatedIssueArg
    {
        public long RiskId { get; set; }
        public long IssueId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
