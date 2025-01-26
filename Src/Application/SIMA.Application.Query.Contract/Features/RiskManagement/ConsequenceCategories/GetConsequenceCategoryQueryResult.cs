namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;

public class GetConsequenceCategoryQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}