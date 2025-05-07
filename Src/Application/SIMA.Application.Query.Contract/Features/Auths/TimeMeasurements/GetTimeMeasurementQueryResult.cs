namespace SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;

public class GetTimeMeasurementQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long UnitBasement { get; set; }
    public string? ActiveStatus { get; set; }
}