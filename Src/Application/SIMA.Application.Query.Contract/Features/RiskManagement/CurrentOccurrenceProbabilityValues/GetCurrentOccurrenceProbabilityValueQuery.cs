using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public class GetCurrentOccurrenceProbabilityValueQuery : IQuery<Result<GetCurrentOccurrenceProbabilityValueQueryResult>>
{
    public long Id { get; set; }
}