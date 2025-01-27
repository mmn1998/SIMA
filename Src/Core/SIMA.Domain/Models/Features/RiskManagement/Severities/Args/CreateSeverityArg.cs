namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Args;

public class CreateSeverityArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long ConsequenceLevelId { get; set; }
    public long SeverityValueId { get; set; }
    public long AffectedHistoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}