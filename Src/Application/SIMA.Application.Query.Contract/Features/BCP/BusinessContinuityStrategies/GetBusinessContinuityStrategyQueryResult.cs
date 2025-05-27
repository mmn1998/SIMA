using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;

public class GetBusinessContinuityStrategyQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDateTime();
    public DateTime? ReviewDate { get; set; }
    public string? ReviewDateRersian => ReviewDate.ToPersianDateTime();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? ActiveStatus { get; set; }

    public IEnumerable<GetBusinessContinuityStratgyDocumentQueryResult>? BusinessContinuityStrategyDocumentList { get; set; }
    public IEnumerable<GetBusinessContinuityStratgyRiskQueryResult>? BusinessContinuityStratgyRiskQueryResult { get; set; }

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



public class GetBusinessContinuityStratgyRiskQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long? RiskCategoryId { get; set; }
    public string? RiskCategoryName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
}