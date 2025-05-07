using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateCobitRiskCategoryScenarioArg
{
    public long Id { get; set; }
    public long RiskId { get; set; }
    public long CobitCategoryId { get; set; }
    public long CobitScenarioId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public string Code { get; set; } = IdHelper.GenerateUniqueId().ToString();
}
