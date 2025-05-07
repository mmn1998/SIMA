namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;

public class ModifyMatrixAArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long MatrixAValueId { get; set; }
    public long TriggerStatusId { get; set; }
    public long UseVulnerabilityId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}