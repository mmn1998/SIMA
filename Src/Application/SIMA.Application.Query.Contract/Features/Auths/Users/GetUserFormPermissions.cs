namespace SIMA.Application.Query.Contract.Features.Auths.Users
{
    public class GetUserFormPermissions
    {
        public GetFormUserQuery Form { get; set; }
        public List<GetUserPermissionQueryResult> Permissions { get; set; }
    }
}
