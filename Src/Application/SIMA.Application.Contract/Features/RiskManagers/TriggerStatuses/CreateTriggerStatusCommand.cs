using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.TriggerStatuses;

public class CreateTriggerStatusCommand: ICommand<Result<long>>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public float? NumericValue { get; set; }
}