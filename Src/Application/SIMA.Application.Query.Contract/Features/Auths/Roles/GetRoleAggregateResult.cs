namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRoleAggregateResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string EnglishKey { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
    public List<GetRolePermissionQueryResultForAggregate> RolePermissions { get; set; }
}
