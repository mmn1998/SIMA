namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;

public class ModifyCurrencyTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsBaseCurrency { get; set; }
    public string? Symbol { get; set; }
    public long? ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
