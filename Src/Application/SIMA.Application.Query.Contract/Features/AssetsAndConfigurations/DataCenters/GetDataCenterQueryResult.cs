namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;

public class GetDataCenterQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}