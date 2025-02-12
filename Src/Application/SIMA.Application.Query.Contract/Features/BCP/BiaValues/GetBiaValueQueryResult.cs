namespace SIMA.Application.Query.Contract.Features.BCP.BiaValues;

public class GetBiaValueQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public string? ConsequenceIntensionName { get; set; }
    public long ConsequenceId { get; set; }
    public string? ConsequenceName { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
}