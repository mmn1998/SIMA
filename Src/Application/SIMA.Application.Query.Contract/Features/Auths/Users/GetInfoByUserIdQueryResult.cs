namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetInfoByUserIdQueryResult
{
    public UserInfo UserInfo { get; set; }
    public List<PhoneResult> Phones { get; set; }
    public List<PositionResult> Positions { get; set; }
    public List<AddressResult> Addresses { get; set; }
}
