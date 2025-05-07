using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class ReturnInfo
{
    public long OrderingItemId { get; set; }
    public int ReturnQuantity { get; set; }
    public string? ReturnBy { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? ReturnDatePersian => DateHelper.ToPersianDateTime(ReturnDate);
    public string? Description { get; set; }
    public long? ReciptDocumentId { get; set; }
    public string? ReciptDocumentName { get; set; }
    public string? DocumentExtensionName { get; set; }
    public string? DocumentContentType { get; set; }
}



