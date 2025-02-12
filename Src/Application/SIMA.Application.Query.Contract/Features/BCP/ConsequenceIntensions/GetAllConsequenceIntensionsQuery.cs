using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;

public class GetAllConsequenceIntensionsQuery : BaseRequest, IQuery<Result<IEnumerable<GetConsequenceIntensionQueryResult>>>
{
}