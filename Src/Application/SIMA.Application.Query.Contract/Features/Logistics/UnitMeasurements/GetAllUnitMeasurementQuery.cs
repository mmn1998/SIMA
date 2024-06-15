using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;

public class GetAllUnitMeasurementQuery : BaseRequest, IQuery<Result<IEnumerable<GetUnitMeasurementQueryResult>>>
{
}