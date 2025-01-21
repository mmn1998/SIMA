using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.WageDeductionMethods;

public class GetWageDeductionMethodQuery : IQuery<Result<GetWageDeductionMethodQueryResult>>
{
    public long Id { get; set; }
}