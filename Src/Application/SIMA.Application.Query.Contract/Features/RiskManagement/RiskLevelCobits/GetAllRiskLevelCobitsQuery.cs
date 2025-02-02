using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;

public class GetAllRiskLevelCobitsQuery : BaseRequest, IQuery<Result<IEnumerable<GetRiskLevelCobitQueryResult>>>
{
}