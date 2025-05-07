using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;

public class GetCobitScenarioQuery : IQuery<Result<GetCobitScenarioQueryResult>>
{
    public long Id { get; set; }
}