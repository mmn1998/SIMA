namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRolePermissionQueryResult
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
}
