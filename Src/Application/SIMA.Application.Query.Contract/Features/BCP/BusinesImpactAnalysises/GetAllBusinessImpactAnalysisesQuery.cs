using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;

public class GetAllBusinessImpactAnalysisesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAllBusinessImpactAnalysisesQueryResult>>>
{
    public long? ServiceId { get; set; }
}