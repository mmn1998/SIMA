using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class DeliveryInfo
{
    public string? DeliveredBy { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string? DeliveryDatePersian => DateHelper.ToPersianDateTime(DeliveryDate);
    public long ReceiptDocumentId { get; set; }
    public string? Description { get; set; }
}



