using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateRiskCommand : ICommand<Result<long>>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long RiskTypeId { get; set; }
    public List<string>? PreventiveActionList { get; set; }
    public List<string>? CorrectiveActionList { get; set; }
    public List<string>? StaffList { get; set; }
    public List<CreateEffectedAssetCommand>? EffectedAssetList { get; set; }
    public List<CreateserviceRiskImpactCommand>? ServiceRiskImpactList { get; set; }
    public List<CreateThreatCommand>? ThreatList { get; set; }
}