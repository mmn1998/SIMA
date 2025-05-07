using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Severities;

public class GetAllSeveritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetSeverityQueryResult>>>
{
}