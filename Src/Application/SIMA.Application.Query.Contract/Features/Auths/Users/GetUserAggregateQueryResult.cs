using SIMA.Application.Query.Contract.Features.Auths.Roles;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserAggregateQueryResult
{
    public long Id { get; set; }
    public long? ProfileId { get; set; }
    public long? CompanyId { get; set; }
    public string Username { get; set; }
    //public List<GetFormUserQuery> FormUsers { get; set; }
    public List<GetUserRoleQueryForAggregate> UserRoles { get; set; }
    public List<GetUserGroupsQuery> UserGroups { get; set; }
    //public List<GetUserPermissionQueryForAggregate> UserPermissions { get; set; }
    public List<GetUserFormPermissions> FormPermissions { get; set; }
    public List<GetUserLocationQueryForAggregate> UserLocations { get; set; }
}

