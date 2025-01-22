namespace SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;

public class GetCurrencyTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsBaseCurrency { get; set; }
    public string? Symbol { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
}
