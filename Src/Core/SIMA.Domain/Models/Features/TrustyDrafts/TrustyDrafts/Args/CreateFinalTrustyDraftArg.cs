namespace SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;

public class CreateFinalTrustyDraftArg
{
    public string DraftNumber { get; set; }
    public string? BranchLetterNumber { get; set; }
    public long Id { get; set; }
    public long? InquiryRequestId { get; set; }
    public long WageDeductionMethodId { get; set; }
    public long IssueId { get; set; }
    public long DraftTypeId { get; set; }
    public decimal? DraftIssueWage { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal? BuyShareFromWage { get; set; }
    public decimal? MainShareFromWage { get; set; }
}
