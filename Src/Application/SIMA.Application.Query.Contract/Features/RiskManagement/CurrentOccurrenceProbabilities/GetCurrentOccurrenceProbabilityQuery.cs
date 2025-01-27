using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class GetCurrentOccurrenceProbabilityQuery : IQuery<Result<GetCurrentOccurrenceProbabilityQueryResult>>
{
    public long Id { get; set; }
}