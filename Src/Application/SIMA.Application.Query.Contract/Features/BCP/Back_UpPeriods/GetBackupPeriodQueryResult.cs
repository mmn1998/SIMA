namespace SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;

public class GetBackupPeriodQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}