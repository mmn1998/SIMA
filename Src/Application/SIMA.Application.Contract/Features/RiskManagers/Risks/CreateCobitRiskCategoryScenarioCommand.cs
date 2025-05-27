namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateCobitRiskCategoryScenarioCommand
{
    public long CobitRiskCategoryId { get; set; }
    public long CobitScenarioId { get; set; }
}
