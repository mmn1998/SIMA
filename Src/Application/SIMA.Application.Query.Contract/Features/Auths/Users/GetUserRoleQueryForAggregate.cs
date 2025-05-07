namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserRoleQueryForAggregate
{
    public long UserRoleId { get; set; }
    public long RoleId { get; set; }
    public string RoleName { get; set; }
}

