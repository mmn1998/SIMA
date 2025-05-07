using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Scenarios
{
    public class GetAllScenariosQuery : BaseRequest, IQuery<Result<IEnumerable<GetScenarioQueryResult>>>
    {
    }
}
