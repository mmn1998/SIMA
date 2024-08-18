namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupPermissionResultForAggregate
{
    public long? PermissionId { get; set; }
    public long? DomainId { get; set; }
    public string? PermissionName { get; set; }
    public string? DomainName { get; set; }
}
