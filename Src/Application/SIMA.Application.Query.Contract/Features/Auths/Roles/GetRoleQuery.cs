using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetRoleQuery : IQuery<Result<GetRoleQueryResult>>
{
    public long Id { get; set; }
}
