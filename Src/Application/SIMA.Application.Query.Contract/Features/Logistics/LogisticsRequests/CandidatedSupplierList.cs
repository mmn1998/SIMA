using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class CandidatedSupplierList
{
    public long SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? IsSelected { get; set; }
    public DateTime? SelectionDate { get; set; }
    public string? SelectionDatePersian => DateHelper.ToPersianDateTime(SelectionDate);
    public string? IsWrittenInquiry { get; set; }
    public float InquieredPrice { get; set; }
    public string? InvoiceDocumentPath { get; set; }
    public long? InvoiceDocumentId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
}



