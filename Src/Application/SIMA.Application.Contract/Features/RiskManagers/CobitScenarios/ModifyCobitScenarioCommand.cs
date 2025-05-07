using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;

public class ModifyCobitScenarioCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long CobitRiskCategoryId { get; set; }
   // public long ScenarioId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? CobitIdentifier { get; set; }
}