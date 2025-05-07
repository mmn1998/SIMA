using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.UIInputElements;

public class GetAllUIInputElementsQuery : BaseRequest, IQuery<Result<IEnumerable<GetUIInputElementQueryResult>>>
{
}