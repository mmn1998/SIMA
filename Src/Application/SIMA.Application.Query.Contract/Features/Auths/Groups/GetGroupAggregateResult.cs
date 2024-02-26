namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupAggregateResult
{
    public GetGroupResultForAggregate Group { get; set; }
    public List<GetGroupPermissionResultForAggregate> GroupPermissions { get; set; }
    public List<GetUserGroupResultForAggregate> UsrGroups { get; set; }
}
