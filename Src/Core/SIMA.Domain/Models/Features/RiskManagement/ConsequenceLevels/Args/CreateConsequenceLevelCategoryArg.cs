namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Args;

public class CreateConsequenceLevelCategoryArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}