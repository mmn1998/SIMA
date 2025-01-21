namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetWageCalculatorQueryResult
{
    public long Id { get; set; }
    public string? IsBasedOnPercentage { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Wage { get; set; }
    public decimal? WageFixedValue { get; set; }
    public int? WagePercentage { get; set; }
    public decimal? FinalWage { get; set; }
}
