namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetProfileByProfileIdQueryResult
{
    public ProfileInfo ProfileInfo { get; set; }
    public List<PhoneResult> Phones { get; set; }
    public List<AddressResult> Addresses { get; set; }
    public List<UserResult> Users { get; set; }
}
