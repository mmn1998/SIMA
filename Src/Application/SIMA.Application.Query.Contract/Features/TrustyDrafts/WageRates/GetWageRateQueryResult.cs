namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetWageRateQueryResult
{
    public long Id { get; set; }
    public long CurrencyOperationTypeId { get; set; }
    public string? CurrencyOperationTypeName { get; set; }
    public long CurrencyTypeId { get; set; }
    public string? CurrencyTypeName { get; set; }
    public long PaymentTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
    public long DraftTypeId { get; set; }
    public string? DraftTypeName { get; set; }
    public long CurrencyPaymentChannelId { get; set; }
    public string? CurrencyPaymentChannelName { get; set; }
    public long? DraftOriginId { get; set; }
    public string? DraftOriginName { get; set; }
    public string? Name { get; set; }
    public string? IsBasedOnPercentage { get; set; }
    public decimal Discount { get; set; }
    public float? WagePercentage { get; set; }
    public decimal? WageFixedValue { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
}