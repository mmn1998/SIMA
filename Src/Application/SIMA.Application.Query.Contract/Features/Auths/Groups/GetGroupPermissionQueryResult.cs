namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupPermissionQueryResult
{
    public long Id { get; set; }
    public long PermissionId { get; set; }
    public long GroupId { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
