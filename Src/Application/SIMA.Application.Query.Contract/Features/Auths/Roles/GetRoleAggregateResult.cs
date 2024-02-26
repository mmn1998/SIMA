namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRoleAggregateResult
{
    public GetRoleQueryResultForAggregate Role { get; set; }
    public List<GetRolePermissionQueryResultForAggregate> RolePermissions { get; set; }
}
