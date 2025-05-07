using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetAllRoleQuery : BaseRequest, IQuery<Result<IEnumerable<GetRoleQueryResult>>>
{
    public long? DomainId { get; set; }
}
