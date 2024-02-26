namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserByProfileIdQueryResult
{
    public int? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? Gender { get; set; }
    public string? GenderCode { get; set; }
    public string? Username { get; set; }
    public string? Company { get; set; }
    public string? CompanyCode { get; set; }
    public string? NationalId { get; set; }
    public string? BirthDate { get; set; }
    public List<PhoneResult> Phones { get; set; }
    public List<PositionResult> Positions { get; set; }
    public List<AddressResult> Addresses { get; set; }
}
