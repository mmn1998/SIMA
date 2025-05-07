using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;

public class GetConsequenceLevelQuery : IQuery<Result<GetConsequenceLevelQueryResult>>
{
    public long Id { get; set; }
}