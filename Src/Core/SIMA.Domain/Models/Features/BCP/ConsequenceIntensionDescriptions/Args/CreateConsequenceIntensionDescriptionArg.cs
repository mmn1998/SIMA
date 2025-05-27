namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Args;

public class CreateConsequenceIntensionDescriptionArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ConsequenceId { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public string Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}