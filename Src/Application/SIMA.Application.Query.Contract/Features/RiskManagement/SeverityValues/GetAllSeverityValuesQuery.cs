using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;

public class GetAllSeverityValuesQuery : BaseRequest, IQuery<Result<IEnumerable<GetSeverityValueQueryResult>>>
{
}