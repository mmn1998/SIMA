using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class OrderingList
{
    public long OrderingId { get; set; }
    public long SupplierId { get; set; }
    public string SupplierName { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? OrderDatePersian => DateHelper.ToPersianDateTime(OrderDate);
    public string? ReceiptNumber { get; set; }
    public long? ReceiptDocumentId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }
}



