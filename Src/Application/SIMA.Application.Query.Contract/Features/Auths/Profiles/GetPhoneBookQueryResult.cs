namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetPhoneBookQueryResult
{
    public long Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneTypeName { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
