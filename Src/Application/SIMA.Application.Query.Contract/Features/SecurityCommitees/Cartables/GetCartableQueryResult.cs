using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.Text.Json.Serialization;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;

public class GetCartableQueryResult : IQuery<Result<GetCartableQuery>>
{
    public long Id { get; set; }
    public long CurrentWorkflowId { get; set; }
    public string WorkFlowName { get; set; }
    public string ProjectName { get; set; }
    public string DomainName { get; set; }
    public long MainAggregateId { get; set; }
    public long SourceId { get; set; }
    public long IssueTypeId { get; set; }
    public string IssueTypeName { get; set; }
    public long IssuePriorityId { get; set; }
    public string IssuePriorityName { get; set; }
    public long IssueWeightCategoryId { get; set; }
    public string IssueWeightCategoryName { get; set; }
    public long CurrentStepId { get; set; }
    public string CurrentStepName { get; set; }
    public long CurrentStateId { get; set; }
    public string CurrentStateName { get; set; }
    public int Weight { get; set; }
    public string Summery { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
    [JsonIgnore]
    public DateTime DueDate { get; set; }
    public string? WorkFlowFileContent { get; set; }
    [JsonIgnore]
    public string? BpmnId { get; set; }
    public string PersianDueDate => DateHelper.ToPersianDate(DueDate);
    public List<GetIssueLinkQueryResult> IssueLinks { get; set; }
    public List<GetIssueDocumentQueryResult> IssueDocuments { get; set; }
    public List<GetIssueCommentQueryResult> IssueComments { get; set; }
    public List<GetRelatedProgressQueryResult> RelatedProgresses { get; set; }
    public string? JsonContent { get; set; }
    public string? FormName { get; set; }
    public string? FormTitle { get; set; }
    public string? IsSystemForm { get; set; }
    public long FormId { get; set; }
}
