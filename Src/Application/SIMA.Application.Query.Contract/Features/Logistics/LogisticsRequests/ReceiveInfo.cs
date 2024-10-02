using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class ReceiveInfo
{
    public string? ReceivedBy { get; set; }
    public string? ReceiptNumber { get; set; }
    public DateTime? ReceiveDate { get; set; }
    public string? ReceiptDatePersian => DateHelper.ToPersianDateTime(ReceiveDate);
    public long ReceiptDocumentId { get; set; }
    public string? Description { get; set; }
}



