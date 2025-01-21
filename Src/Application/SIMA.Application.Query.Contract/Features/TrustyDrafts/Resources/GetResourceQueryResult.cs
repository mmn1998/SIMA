namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;

public class GetResourceQueryResult
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string? ParentTitle { get; set; }
    public long AccountTypeId { get; set; }
    public string? AccountTypeName { get; set; }
    public long BrokerId { get; set; }
    public string? BrokerName { get; set; }
    public long CurrencyTypeId { get; set; }
    public string? CurrencyTypeName { get; set; }
    public string? Title { get; set; }
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvaliableBalance { get; set; }
    public decimal BlockedBalance { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}