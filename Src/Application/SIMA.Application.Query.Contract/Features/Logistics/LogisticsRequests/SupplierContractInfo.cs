using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class SupplierContractInfo
{
    public long Id { get; set; }
    public string? ContractNumber { get; set; }
    public DateTime ContractDate { get; set; }
    public string? ContractDatePersian => DateHelper.ToPersianDateTime(ContractDate);
    public long ContractDocumentId { get; set; }
    public string? Description { get; set; }
}



