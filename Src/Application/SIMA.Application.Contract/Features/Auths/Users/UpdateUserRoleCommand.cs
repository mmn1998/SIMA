using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Users;

public class UpdateUserRoleCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public string? IsActive { get; set; }
    public long? ModifiedBy { get; set; }
    public long ActiveStatusId { get; set; }
   
}
