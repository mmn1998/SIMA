namespace SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;

public class GetMatrixAQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long? UseVulnerabilityId { get; set; }
    public string? UseVulnerabilityName { get; set; }
    public long? MatrixAValueId { get; set; }
    public string? MatrixAValueName { get; set; }
    public long? TriggerStatusId { get; set; }
    public string? TriggerStatusName { get; set; }
    public string? ActiveStatus { get; set; }
}