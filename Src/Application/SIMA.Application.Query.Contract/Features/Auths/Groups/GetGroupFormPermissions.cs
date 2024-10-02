namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetGroupFormPermissions
{
    public List<GetGroupPermissionQueryResult> Permissions { get; set; }
    public GetFormGroupQuery Form { get; set; }
}
