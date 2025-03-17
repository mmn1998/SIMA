namespace SIMA.Domain.Models.Features.BCP.AnalysisValues.Args;

public class ModifyAnalysisValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ConsequenceId { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public string Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}