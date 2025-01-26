using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.AffectedHistories;

public class GetAllAffectedHistoriesQuery : BaseRequest, IQuery<Result<IEnumerable<GetAffectedHistoryQueryResult>>>
{
}