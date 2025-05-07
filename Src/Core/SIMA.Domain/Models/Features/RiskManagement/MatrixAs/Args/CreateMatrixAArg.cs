namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;

public class CreateMatrixAArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long MatrixAValueId { get; set; }
    public long TriggerStatusId { get; set; }
    public long UseVulnerabilityId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}