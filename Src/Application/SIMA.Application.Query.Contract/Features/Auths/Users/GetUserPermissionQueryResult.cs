namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserPermissionQueryResult
{
    public long UserId { get; set; }
    public long PermissionId { get; set; }
    public long FormId { get; set; }
    public string FormName { get; set; }
    public string FormTitle { get; set; }
    public string PermissionName { get; set; }
}

