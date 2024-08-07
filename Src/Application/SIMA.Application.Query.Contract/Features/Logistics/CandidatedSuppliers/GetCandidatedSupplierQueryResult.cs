using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;

public class GetCandidatedSupplierQueryResult
{
    public long Id { get; set; }
    /// <summary>
    /// name of supplier
    /// </summary>
    public string? Name { get; set; } 
    public string? IsSelected { get; set; }
    public long LogisticsRequestId { get; set; }
    public long SupplierId { get; set; }
    public DateTime? SelectedDate { get; set; }
    public string? SelectedDatePersian => SelectedDate.ToPersianDateTime();
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public decimal InquieredPrice { get; set; }
}
