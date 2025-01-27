namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Args;

public class ModifySeverityArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public long SeverityValueId { get; set; }
    public long AffectedHistoryId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}