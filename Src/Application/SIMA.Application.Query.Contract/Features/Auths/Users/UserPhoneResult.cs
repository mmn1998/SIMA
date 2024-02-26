namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public record UserPhoneResult
{
    public long PhoneNumberId { get; init; }
    public string PhoneNumber { get; init; }
    public string PhoneType { get; init; }
    public string PhoneTypeCode { get; init; }
}
