using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class ReturnInfo
{
    public string? ReturnBy { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? ReturnDatePersian => DateHelper.ToPersianDateTime(ReturnDate);
    public string? Description { get; set; }
}



