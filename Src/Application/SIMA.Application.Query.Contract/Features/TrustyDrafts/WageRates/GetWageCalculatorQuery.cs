using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetWageCalculatorQuery : IQuery<Result<GetWageCalculatorQueryResult>>
{
    public long WageRateId { get; set; }
    public decimal Amount { get; set; }
    public long CurrencyTypeId { get; set; }
}