namespace SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;

public class ModifySolutionPriorityArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float Priority { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}