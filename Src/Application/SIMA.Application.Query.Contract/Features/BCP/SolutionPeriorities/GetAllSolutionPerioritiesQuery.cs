using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;

public class GetAllSolutionPerioritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetSolutionPeriorityQueryResult>>>
{
}