namespace SIMA.Application.Query.Contract.Features.RiskManagement.Severities;

public class GetSeverityQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long? AffectedHistoryId { get; set; }
    public string? AffectedHistoryName { get; set; }
    public long? SeverityValueId { get; set; }
    public string? SeverityValueName { get; set; }
    public long? ConsequenceLevelId { get; set; }
    public string? ConsequenceLevelName { get; set; }
    public string? ActiveStatus { get; set; }
}