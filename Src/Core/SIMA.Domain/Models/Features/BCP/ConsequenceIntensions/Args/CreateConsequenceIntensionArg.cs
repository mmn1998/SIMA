namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Args;

public class CreateConsequenceIntensionArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float ValueNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}