using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;

public class GetTimeMeasurementQuery : IQuery<Result<GetTimeMeasurementQueryResult>>
{
    public long Id { get; set; }
}