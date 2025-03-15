namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;

public class GetBackupMethodQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}