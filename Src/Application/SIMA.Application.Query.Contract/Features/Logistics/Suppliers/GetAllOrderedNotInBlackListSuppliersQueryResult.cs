namespace SIMA.Application.Query.Contract.Features.Logistics.Suppliers;

public class GetAllOrderedNotInBlackListSuppliersQueryResult
{
   
    public long SupplierId { get; set; }
    public long Id { get; set; }
    public long SupplierRankId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierRankName { get; set; }
}
