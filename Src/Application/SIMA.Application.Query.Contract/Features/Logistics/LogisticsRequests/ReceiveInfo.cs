using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class ReceiveInfo
{
    public long OrderingId { get; set; }
    public DateTime? ReceiveDate { get; set; }
    public string? ReceiptDatePersian => DateHelper.ToPersianDateTime(ReceiveDate);
    public string? ReceiptNumber { get; set; }
    public long ReceiptDocumentId { get; set; }
    public string? ReceiptDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
    public string? Description { get; set; }
    public string? ReceivedBy { get; set; }
}



