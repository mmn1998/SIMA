using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilities;

public class GetInherentOccurrenceProbabilityQuery : IQuery<Result<GetInherentOccurrenceProbabilityQueryResult>>
{
    public long Id { get; set; }
}