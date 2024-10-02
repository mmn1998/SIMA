using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class DocumentList
{
    public long Id { get; set; }
    public string? DocumentPath { get; set; }
    public string? DocumentFileName { get; set; }
    public string? DocumentTypeName { get; set; }
    public long DocumentTypeId { get; set; }
    public long FileExtensionId { get; set; }
    public string? DocumentContentType { get; set; }
    public string? DocumentExtensionName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }
    public string? AttachStepName { get; set; }
}



