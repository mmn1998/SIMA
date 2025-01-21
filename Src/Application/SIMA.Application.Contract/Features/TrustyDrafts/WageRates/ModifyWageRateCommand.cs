using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.WageRates;

public class ModifyWageRateCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long CurrencyOperationTypeId { get; set; }
    public long CurrencyTypeId { get; set; }
    public long PaymentTypeId { get; set; }
    public long DraftTypeId { get; set; }
    public long? DraftOriginId { get; set; }
    public long CurrencyPaymentChannelId { get; set; }
    public string Name { get; set; }
    public string IsBasedOnPercentage { get; set; }
    public decimal? Discount { get; set; }
    public float? WagePercentage { get; set; }
    public decimal? WageFixedValue { get; set; }
    public string? Description { get; set; }
}