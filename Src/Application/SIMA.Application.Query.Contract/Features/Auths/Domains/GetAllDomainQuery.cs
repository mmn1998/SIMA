using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Domains;

public class GetAllDomainQuery : BaseRequest, IQuery<Result<List<GetDomainQueryResult>>>
{
}
