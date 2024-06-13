using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.UnitMeasurements;

public class DeleteUnitMeasurementCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}