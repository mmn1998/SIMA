namespace SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Args;

public class CreateHappeningPossibilityArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }
    public float Ordering { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}