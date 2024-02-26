using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Domains;

public class GetAllDomainQuery : IQuery<Result<List<GetDomainQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
