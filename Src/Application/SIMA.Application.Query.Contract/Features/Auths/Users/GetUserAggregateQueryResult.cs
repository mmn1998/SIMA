namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserAggregateQueryResult
{
    public GetUserQueryForAggregate User { get; set; }
    public List<GetUserDomainQueryForAggregate> UserDomains { get; set; }
    public List<GetUserRoleQueryForAggregate> UserRoles { get; set; }
    public List<GetUserPermissionQueryForAggregate> UserPermissions { get; set; }
    public List<GetUserLocationQueryForAggregate> UserLocations { get; set; }
}

