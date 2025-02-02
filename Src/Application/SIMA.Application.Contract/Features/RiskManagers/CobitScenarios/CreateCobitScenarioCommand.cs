using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;

public class CreateCobitScenarioCommand : ICommand<Result<long>>
{
    public long CobitScenarioCategoryId { get; set; }
    public long ScenarioId { get; set; }
    public string CobitIdentifier { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}