using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes
{
    public class GetAllThreatTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetThreatTypesQueryResult>>>
    {
    }
}
