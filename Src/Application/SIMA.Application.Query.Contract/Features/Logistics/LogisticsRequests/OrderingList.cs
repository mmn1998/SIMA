using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class OrderingList
{
    public DateTime? OrderDate { get; set; }
    public string? OrderDatePersian => DateHelper.ToPersianDateTime(OrderDate);
    public string? ReceiptNumber { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? IsContractRequired { get; set; }
    public string? IsPrePaymentRequired { get; set; }
    public string? CreatedBy { get; set; }
    public long? DocumentId { get; set; }
}



