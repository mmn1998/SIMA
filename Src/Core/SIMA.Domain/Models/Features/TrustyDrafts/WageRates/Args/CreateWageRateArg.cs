namespace SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Args;

public class CreateWageRateArg
{
    public long Id { get; set; }
    public long CurrencyOperationTypeId { get; set; }
    public long CurrencyTypeId { get; set; }
    public long PaymentTypeId { get; set; }
    public long DraftTypeId { get; set; }
    public long CurrencyPaymentChannelId { get; set; }
    public long DraftOriginId { get; set; }
    public string Name { get; set; }
    public string IsBasedOnPercentage { get; set; }
    public decimal Discount { get; set; }
    public float? WagePercentage { get; set; }
    public decimal? WageFixedValue { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}