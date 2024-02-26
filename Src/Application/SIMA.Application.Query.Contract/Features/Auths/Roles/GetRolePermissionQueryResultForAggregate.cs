namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRolePermissionQueryResultForAggregate
{
    public long? DomainId { get; set; }
    public string? DomainName { get; set; }
    public long? PermissionId { get; set; }
    public string? PermissionName { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
