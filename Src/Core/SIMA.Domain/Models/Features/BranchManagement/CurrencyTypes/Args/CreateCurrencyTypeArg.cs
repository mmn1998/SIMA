namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;

public class CreateCurrencyTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsBaseCurrency { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
