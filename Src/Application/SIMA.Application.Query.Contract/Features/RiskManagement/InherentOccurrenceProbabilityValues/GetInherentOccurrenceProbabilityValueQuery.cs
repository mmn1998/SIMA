using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilityValues;

public class GetInherentOccurrenceProbabilityValueQuery : IQuery<Result<GetInherentOccurrenceProbabilityValueQueryResult>>
{
    public long Id { get; set; }
}