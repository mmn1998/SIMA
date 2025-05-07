using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes
{
    public class GetRiskTypeQuery : IQuery<Result<GetRiskTypesQueryResult>>
    {
        public long Id { get; set; }
    }
}
