namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args
{
    public class CreateRiskRelatedIssueArg
    {
        public long RiskId { get; set; }
        public long IssueId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
