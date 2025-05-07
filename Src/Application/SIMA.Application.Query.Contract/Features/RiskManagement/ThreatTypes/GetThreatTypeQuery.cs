using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.ThreatTypes
{
    public class GetThreatTypeQuery : IQuery<Result<GetThreatTypesQueryResult>>
    {
        public long Id { get; set; }
    }
}
