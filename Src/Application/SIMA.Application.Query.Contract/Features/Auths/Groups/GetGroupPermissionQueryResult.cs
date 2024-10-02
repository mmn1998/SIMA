namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupPermissionQueryResult
{
    public long GroupId { get; set; }
    public long FormId { get; set; }
    public string? FormName { get; set; }
    public string? FormTitle { get; set; }
    public long PermissionId { get; set; }
    public string? PermissionName { get; set; }
}
