namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;

public class ModifyConsequenceLevelArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}