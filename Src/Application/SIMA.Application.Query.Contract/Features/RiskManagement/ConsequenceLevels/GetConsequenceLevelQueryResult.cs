namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;

public class GetConsequenceLevelQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? ConsequenceCategoryName { get; set; }
    public long? ConsequenceCategoryId { get; set; }
    public float? NumericValue { get; set; }
    public string? ActiveStatus { get; set; }
}