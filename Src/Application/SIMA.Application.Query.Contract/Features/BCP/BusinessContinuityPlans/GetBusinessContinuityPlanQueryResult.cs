using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;

public class GetBusinessContinuityPlanQueryResult
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Scope { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyIssue>? BusinessContinuityStratgyIssueList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanversioning>? BusinesscontinuityplanVersionList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanstratgy>? BusinesscontinuityplanStratgyList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanservice>? BusinesscontinuityplanServiceList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanrisk>? BusinesscontinuityplanRiskList { get; set; }
    public IEnumerable<GetBusinesscontinuityplancriticalactivity>? BusinesscontinuityplanCriticalActivityList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanrelatedstaff>? BusinesscontinuityplanRelatedstaffList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanresponsible>? BusinesscontinuityplanResponsibleList { get; set; }
    public IEnumerable<GetBusinesscontinuityplanassumption>? BusinesscontinuityplanAssumptionList { get; set; }
   
}public class GetAllBusinessContinuityPlansQueryResult
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Scope { get; set; }
    public string? VersionNumber { get; set; }
    public string? ReleaseDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public long? IssueId { get; set; }
    public string? IssueCode { get; set; }
    public long? WorkflowId { get; set; }
    public string? WorkflowName { get; set; }
    public long? CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public long? CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public string? IssueCreatedAt { get; set; }
    public string? IssueCreatedBy { get; set; }
    public string? CompanyName { get; set; }   
}

