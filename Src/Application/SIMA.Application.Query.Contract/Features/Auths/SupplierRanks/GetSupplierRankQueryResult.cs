namespace SIMA.Application.Query.Contract.Features.Auths.SupplierRanks;

public class GetSupplierRankQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float Ordering { get; set; }
    public string? ActiveStatus { get; set; }
    public int? SupplierSuccessOrderCountForm { get; set; }
    public int? SupplierSuccessOrderCountTo { get; set; }
}