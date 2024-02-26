namespace SIMA.Application.Query.Contract.Features.Auths.Permission;

public class GetPermissionQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
    public bool IsDeactivate { get; set; }
    public bool IsDeleted { get; set; }
}
