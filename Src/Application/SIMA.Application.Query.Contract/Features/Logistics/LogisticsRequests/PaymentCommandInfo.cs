using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class PaymentCommandInfo
{
    public long Id { get; set; }
    public long? OrderingId { get; set; }
    public DateTime CommandDate { get; set; }
    public string CommandDatePersian  => DateHelper.ToPersianDateTime(CommandDate);
    public string? CommandDescription { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
    public string? CreatedBy { get; set; }

}



