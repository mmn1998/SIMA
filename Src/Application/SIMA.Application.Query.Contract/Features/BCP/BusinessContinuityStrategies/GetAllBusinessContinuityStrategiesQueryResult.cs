using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;

public class GetAllBusinessContinuityStrategiesQueryResult
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDateTime();
    public DateTime? ReviewDate { get; set; }
    public string? ReviewDateRersian => ReviewDate.ToPersianDateTime();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? ActiveStatus { get; set; }
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
}


