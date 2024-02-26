using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRoleAggregate : IQuery<Result<GetRoleAggregateResult>>
{
    public long RoleId { get; set; }
}
