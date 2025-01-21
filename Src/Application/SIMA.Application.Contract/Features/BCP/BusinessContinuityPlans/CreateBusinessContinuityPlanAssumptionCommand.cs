using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans
{
    public class CreateBusinessContinuityPlanAssumptionCommand
    {
        public string Code { get; set; } = IdHelper.GenerateUniqueId().ToString();
        public string Title { get; set; }
    }
}
