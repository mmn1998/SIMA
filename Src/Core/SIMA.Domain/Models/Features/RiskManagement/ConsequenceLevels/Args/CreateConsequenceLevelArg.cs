namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;

public class CreateConsequenceLevelArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public string? Description { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
 }