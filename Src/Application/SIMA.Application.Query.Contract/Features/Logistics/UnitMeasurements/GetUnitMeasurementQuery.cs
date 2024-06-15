using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;

public class GetUnitMeasurementQuery : IQuery<Result<GetUnitMeasurementQueryResult>>
{
    public long Id { get; set; }
}