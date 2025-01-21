using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;

public class GetAllReconsilationTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetReconsilationTypeQueryResult>>>
{
}