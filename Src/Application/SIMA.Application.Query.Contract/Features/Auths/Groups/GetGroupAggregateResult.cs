namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupAggregateResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
    public List<GetGroupPermissionResultForAggregate> GroupPermissions { get; set; }
    public List<GetUserGroupResultForAggregate> UsrGroups { get; set; }
}
