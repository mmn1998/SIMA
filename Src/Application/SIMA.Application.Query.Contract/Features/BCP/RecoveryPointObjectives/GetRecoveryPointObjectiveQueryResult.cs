namespace SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;

public class GetRecoveryPointObjectiveQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int RpoFrom { get; set; }
    public int RpoTo { get; set; }
    public string? ActiveStatus { get; set; }
    public long TimeMeasurementId { get; set; }
    public string? TimeMeasurementName { get; set; }
}