namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueLinkQueryResult
{
    public long IssueIdLinkedTo { get; set; }
    public string IssueSummeryLinkedTo { get; set; }
    public long IssueLinkReasonId { get; set; }
    public string IssueLinkReasonName { get; set; }
    public string IssueLinkReasonCode { get; set; }
}
