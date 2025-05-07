using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Consequences;

public class GetAllConsequencesQuery : BaseRequest, IQuery<Result<IEnumerable<GetConsequenceQueryResult>>>
{
}