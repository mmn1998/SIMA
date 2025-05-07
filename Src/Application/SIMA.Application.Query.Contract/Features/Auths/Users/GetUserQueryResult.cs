namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserQueryResult
{
    public long Id { get; set; }
    public string? CompanyName { get; set; }
    public string? FullName { get; set; }
    public string? Username { get; set; }
    public bool IsDeactivated { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }

}
