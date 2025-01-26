using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;

public class GetAllConsequenceLevelsQuery : BaseRequest, IQuery<Result<IEnumerable<GetConsequenceLevelQueryResult>>>
{
}