namespace SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;

public class CreateSolutionPeriorityArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}