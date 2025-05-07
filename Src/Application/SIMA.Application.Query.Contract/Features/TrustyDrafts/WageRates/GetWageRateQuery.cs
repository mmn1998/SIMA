using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;

public class GetWageRateQuery : IQuery<Result<GetWageRateQueryResult>>
{
    public long Id { get; set; }
}