namespace SIMA.Application.Query.Contract.Features.Logistics.UnitMeasurements;

public class GetUnitMeasurementQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}