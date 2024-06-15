namespace SIMA.Application.Query.Contract.Features.Logistics.Suppliers;

public class GetSupplierQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public long SupplierRankId { get; set; }
    public string? SupplierRank { get; set; }
    public string? IsInBlackList { get; set; }
    public string? ActiveStatus { get; set; }
}
