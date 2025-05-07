namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRolePermissionQueryResult
{
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
    public long FormId { get; set; }
    public string FormName { get; set; }
    public string FormTitle { get; set; }
    public string PermissionName { get; set; }
}
