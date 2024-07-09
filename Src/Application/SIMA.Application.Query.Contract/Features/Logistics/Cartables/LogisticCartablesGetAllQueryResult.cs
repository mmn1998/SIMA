using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.Cartables;

public class LogisticCartablesGetAllQueryResult
{
    public long IssueId { get; set; }
    public string IssueCode { get; set; }
    public long Id { get; set; }
    public string LogesticCode { get; set; }
    public long WorkflowId { get; set; }
    public string WorkFlowName { get; set; }
    public long MainAggregateId { get; set; }
    public string IssueTypeName { get; set; }
    public string IssuePriorityName { get; set; }
    public int Weight { get; set; }
    public string CurrentStateName { get; set; }
    public string CurrentStepName { get; set; }
    public string IssueSummery { get; set; }
    public string IssueDescription { get; set; }
    public string LogesticDescription { get; set; }
    public string? ActiveStatus { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CreateFullName => $"{FirstName} {LastName}";
    public DateTime CreatedAt { get; set; }
    public string PersianCreatedAt => DateHelper.ToPersianDate(CreatedAt);

}





