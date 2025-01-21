namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueComponentQueryResult
{
    public IssueInfo? IssueInfo { get; set; }
    public List<IssueApprovalList>? IssueApprovalList { get; set; }
    public IEnumerable<GetRelatedProgressQueryResult>? RelatedProgressList { get; set; }
    public IEnumerable<GetApprovalOptionQueryResult>? ApprovalOptionList { get; set; }
    public IEnumerable<GetStepRequiredDocumentQueryResult>? StepRequiredDocumentList { get; set; }
    public string? UIPropertyBoxTitle { get; set; }
    public IEnumerable<StoreProcedureParams>? FormParams { get; set; }
    //public string? IsEditable { get; set; }
}
