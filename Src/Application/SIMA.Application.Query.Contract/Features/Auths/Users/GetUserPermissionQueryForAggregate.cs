namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserPermissionQueryForAggregate
{
    public long UserPermissionId { get; set; }
    public long PermissionId { get; set; }
    public long DomainId { get; set; }
    public string DomainName { get; set; }
    public string PermissionName { get; set; }
}
