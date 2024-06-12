using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes
{
    public class GetAllRiskTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetRiskTypesQueryResult>>>
    {
    }
}
