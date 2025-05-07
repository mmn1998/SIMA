namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers;

public class GetAllOrderedNotInBlackListSuppliersQueryResult
{

    public long SupplierId { get; set; }
    public long Id { get; set; }
    public int Index { get; set; }
    public long SupplierRankId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierRankName { get; set; }
    public string? Name { get; set; }
}
