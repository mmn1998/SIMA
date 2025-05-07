using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetAllWageRatesQuery : BaseRequest, IQuery<Result<IEnumerable<GetWageRateQueryResult>>>
{
}