namespace SIMA.Domain.Models.Features.TrustyDrafts.Resources.Args;

public class ModifyResourceArg
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long AccountTypeId { get; set; }
    public long BrokerId { get; set; }
    public long CurrencyTypeId { get; set; }
    public string? Title { get; set; }
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvaliableBalance { get; set; }
    public decimal BlockedBalance { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}