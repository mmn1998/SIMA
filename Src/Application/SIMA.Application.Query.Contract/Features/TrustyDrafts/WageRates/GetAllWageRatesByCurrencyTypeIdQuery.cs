using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetAllWageRatesByCurrencyTypeIdQuery : IQuery<Result<IEnumerable<GetWageRateQueryResult>>>
{
    public long CurrencyTypeId { get; set; }
}