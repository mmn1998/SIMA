namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;

public class CreateConsequenceCategoryArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}