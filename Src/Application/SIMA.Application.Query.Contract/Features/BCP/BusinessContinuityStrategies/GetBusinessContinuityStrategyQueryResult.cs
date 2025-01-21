using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;

public class GetBusinessContinuityStrategyQueryResult
{
    public long Id { get; set; }
    public long StrategyTypeId { get; set; }
    public string? StrategyType‌Name { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? IsStableStrategy { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDateTime();
    public DateTime? ReviewDate { get; set; }
    public string? ReviewDateRersian => ReviewDate.ToPersianDateTime();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? ActiveStatus { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyObjectiveQueryResult>? BusinessContinuityStrategyObjectiveList { get; set; }
    public IEnumerable<GetBusinessContinuityStratgySolutionQueryResult>? BusinessContinuityStrategySolutionList { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyRelatedIssuQueryResult>? BusinessContinuityStrategyRelatedIssueList { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyDocumentQueryResult>? BusinessContinuityStrategyDocumentList { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyResponsibleQueryResult>? BusinessContinuityStrategyResponsibleList { get; set; }
}
public class GetBusinessContinuityStratgyObjectiveQueryResult
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessContinuityStratgySolutionQueryResult
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessContinuityStratgyRelatedIssuQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessContinuityStratgyDocumentQueryResult
{
    public long Id { get; set; }
    public long DocumentTypeId { get; set; }
    public long DocumentId { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? DocumentFileName { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? AttachStepName { get; set; }
}
public class GetBusinessContinuityStratgyResponsibleQueryResult
{
    public long Id { get; set; }
    public string? FullName { get; set; }
    public long PlanResponsibilityId { get; set; }
    public string? PlanResponsibilityName { get; set; }
    public long PositionId { get; set; }
    public string? PositionName { get; set; }
    public long DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string? IsForBackup { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
