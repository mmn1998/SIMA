using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Roles;

public class GetAllRoleQuery : IQuery<Result<List<GetRoleQueryResult>>>
{
    public BaseRequest Request { get; set; }
    public long? DomainId { get; set; }
}
