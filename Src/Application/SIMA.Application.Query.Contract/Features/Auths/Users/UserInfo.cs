namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class UserInfo
{
    public long? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? Gender { get; set; }
    public string? GenderCode { get; set; }
    public string? Username { get; set; }
    public long? CompanyId { get; set; }
    public string? Company { get; set; }
    public string? CompanyCode { get; set; }
    public string? NationalId { get; set; }
    public string? BirthDate { get; set; }
    public string? Brithday { get; set; }
}
