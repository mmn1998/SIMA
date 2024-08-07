using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using System.Diagnostics;

namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class LoginUserQuery : IQuery<Result<LoginUserQueryResult>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class LoginUserModel
{
    public string Username { get; set; }
    public string CompanyName { get; set; }
    public long CompanyId { get; set; }
    public long UserId { get; set; }
    public IEnumerable<long> GroupIds { get; set; }
    public IEnumerable<long> RoleIds { get; set; }
    public IEnumerable<long> PermissionIds { get; set; }

}
public class LoginUserQueryResult
{
    public string Token { get; set; }
    public UserInfoLogin UserInfoLogin { get; set; }
    //public List<Permissions> Permission{ get; set; }
    public IEnumerable<int> Permissions { get; set; }
    public IEnumerable<Menue> TempMenues { get; set; }
    public List<Menue> Menue { get; set; }
    public IEnumerable<long> RoleIds { get; set; }
    public IEnumerable<long> GroupIds { get; set; }
}
public class UserInfoLogin
{
    public string Username { get; set; }
    public long CompanyId { get; set; }
    public long UserId { get; set; }
    public string IsFirstLogin { get; set; }
    public string IsLocked { get; set; }
    public int AccessFailedCount { get; set; }
    public int AccessFailedOverallCount { get; set; }
    public DateTime? AccessFailedDate { get; set; }
}
public class Menue
{
    public long DomainId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public IEnumerable<SubMenue> SubMenues { get; set; }
}
public class SubMenue
{
    public long DomainId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}