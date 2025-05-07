using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftTypes;

public class GetAllDraftTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetDraftTypeQueryResult>>>
{
}