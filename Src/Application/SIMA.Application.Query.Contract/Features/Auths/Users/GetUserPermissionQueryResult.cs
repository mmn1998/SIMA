namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserPermissionQueryResult
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PermissionId { get; set; }
}

