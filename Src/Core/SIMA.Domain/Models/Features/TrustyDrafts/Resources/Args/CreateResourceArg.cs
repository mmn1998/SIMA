namespace SIMA.Domain.Models.Features.TrustyDrafts.Resources.Args;

public class CreateResourceArg
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
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}