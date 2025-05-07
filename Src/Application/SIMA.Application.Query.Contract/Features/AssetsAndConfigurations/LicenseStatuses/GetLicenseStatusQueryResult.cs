namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;

public class GetLicenseStatusQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}