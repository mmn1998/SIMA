namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers;

public class GetSupplierQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long SupplierRankId { get; set; }
    public string? SupplierRank { get; set; }
    public string? IsInBlackList { get; set; }
    public string? ActiveStatus { get; set; }
    public List<GetSupplierAccountListQuery> AccountList { get; set; }
    public List<GetSupplierAddressBookQuery> AddressBooks { get; set; }
    public List<GetSupplierPhoneBookQuery> PhoneBooks { get; set; }
}
