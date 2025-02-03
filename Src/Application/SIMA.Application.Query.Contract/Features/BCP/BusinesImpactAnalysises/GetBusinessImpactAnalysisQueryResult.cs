using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;

public class GetBusinessImpactAnalysisQueryResult
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public long ImportanceDegreeId { get; set; }
    public string? ImportanceDegreeName { get; set; }
    public long ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public long BackupPeriodId { get; set; }
    public string? BackupPeriodName { get; set; }
    public string? RestartReason { get; set; }
    public string? ActiveStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public IEnumerable<GetBusinessImpactAnalysisAssetQueryResult>? BusinessImpactAnalysisAssetList { get; set; }
    public IEnumerable<GetBusinessImpactAnalysisStaffQueryResult>? BusinessImpactAnalysisStaffList { get; set; }
    public IEnumerable<GetBusinessImpactAnalysisDocumentQueryResult>? BusinessImpactAnalysisDocumemntList { get; set; }
    public IEnumerable<GetBusinessImpactAnalysisIssueQueryResult>? BusinessImpactAnalysisIssueList { get; set; }
    public IEnumerable<GetBusinessImpactAnalysisDisasterOriginQueryResult>? BusinessImpactAnalysisDisasterOriginList { get; set; }
}
public class GetBusinessImpactAnalysisAssetQueryResult
{
    public long Id { get; set; }
    public string? AssetName { get; set; }
    public string? AssetCode { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessImpactAnalysisStaffQueryResult
{
    public long Id { get; set; }
    public string? FullName { get; set; }
    public long PositionId { get; set; }
    public string? PositionName { get; set; }
    public long DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessImpactAnalysisDocumentQueryResult
{
    public long Id { get; set; }
    public long DocumentId { get; set; }
    public long DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? DocumentFileName { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? AttachStepName { get; set; }
    public string? CreatedBy { get; set; }
}
public class GetBusinessImpactAnalysisIssueQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
public class GetBusinessImpactAnalysisDisasterOriginQueryResult
{
    public long Id { get; set; }
    public long OriginId { get; set; }
    public string? OriginName { get; set; }
    public string? HappeningPossibilityName { get; set; }
    public long ConsequenceId { get; set; }
    public string? ConsequenceName { get; set; }
    public long RecoveryPointObjectiveId { get; set; }
    public string? RecoveryPointObjectiveName { get; set; }
    public long TimeMeasurementId { get; set; }
    public string? TimeMeasurementName { get; set; }
    public float? MTD { get; set; }
    public float? WRT { get; set; }
    public float? RTO { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}
