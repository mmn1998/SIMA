namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;

public class GetConsequenceIntensionDescriptionQueryResult
{
    public long Id { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public string? ConsequenceIntensionName { get; set; }
    public long ConsequenceId { get; set; }
    public string? ConsequenceName { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
}