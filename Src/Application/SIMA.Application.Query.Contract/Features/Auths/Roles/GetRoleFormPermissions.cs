namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRoleFormPermissions
{
    public GetFormRoleQuery Form { get; set; }
    public List<GetRolePermissionQueryResult> Permissions { get; set; }
}
