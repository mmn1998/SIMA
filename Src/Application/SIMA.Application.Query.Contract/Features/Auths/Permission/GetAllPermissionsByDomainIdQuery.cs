using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Permission;

public class GetAllPermissionsByDomainIdQuery : IQuery<Result<List<GetPermissionQueryResult>>>
{
    public long? DomainId { get; set; }
}
