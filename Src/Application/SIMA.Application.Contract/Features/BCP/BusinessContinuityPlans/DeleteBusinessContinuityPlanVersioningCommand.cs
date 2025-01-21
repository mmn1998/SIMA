using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans
{
    public class DeleteBusinessContinuityPlanVersioningCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long BusinessContinuityPlanVersioningId { get; set; }
    }
}
