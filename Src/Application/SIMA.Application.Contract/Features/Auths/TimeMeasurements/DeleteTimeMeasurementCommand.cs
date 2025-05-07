using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.TimeMeasurements;

public class DeleteTimeMeasurementCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}