namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args;

public class CreateBusinessContinuityStratgySolutionArg
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Code { get; set; }
    public long BusinessContinuityStratgyId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
