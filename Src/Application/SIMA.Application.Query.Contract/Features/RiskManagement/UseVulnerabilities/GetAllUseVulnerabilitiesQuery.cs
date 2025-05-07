using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.UseVulnerabilities;

public class GetAllUseVulnerabilitiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetUseVulnerabilityQueryResult>>>
{
    
}