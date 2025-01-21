using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;

public class GetAllRequestValorsQuery : BaseRequest, IQuery<Result<IEnumerable<GetRequestValorQueryResult>>>
{
}