using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans
{
    public class GetBusinessContinuityPlanQuery : IQuery<Result<GetBusinessContinuityPlanQueryResult>>
    {
        public long Id { get; set; }
    }
}
