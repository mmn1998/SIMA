namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class UserProfileAddressResult
{
    public long ProfileId { get; init; }
    public string ProfileName { get; init; }
    public long AddressId { get; init; }
    public string Address { get; init; }
    public string AddressType { get; init; }
    public string AddressTypeCode { get; init; }
    public string City { get; init; }
    public string CityCode { get; init; }
    public string PostalCode { get; init; }
    public string Province { get; init; }
    public string ProvinceCode { get; init; }
}
