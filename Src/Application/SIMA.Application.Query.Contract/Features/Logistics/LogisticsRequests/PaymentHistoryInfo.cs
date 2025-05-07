using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class PaymentHistoryInfo
{
    public long? Id { get; set; }
    public long? OrderingId { get; set; }
    public long PaymentCommandId { get; set; }
    public string? PaymentNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentDatePersian => DateHelper.ToPersianDateTime(PaymentDate);
    public long? PaymentDocumentId { get; set; }
    public string? PaymentDescription { get; set; }
    public long PaymentTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
    public double PaymentValue { get; set; }
    public string? CreatedBy { get; set; }
}



