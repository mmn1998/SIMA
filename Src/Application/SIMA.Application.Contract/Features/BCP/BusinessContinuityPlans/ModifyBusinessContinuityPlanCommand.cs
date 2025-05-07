using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans
{
    public class ModifyBusinessContinuityPlanCommand: ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long? BusinessContinuityPlanId { get; set; }
        public string? VersionNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<CreateBusinessContinuityPlanStratgyCommand>? BusinessContinuityPlanStratgyList { get; set; }
        public List<CreateBusinessContinuityPlanServiceCommand>? BusinessContinuityPlanServiceList { get; set; }
        public List<CreateBusinessContinuityPlanRiskCommand>? BusinessContinuityPlanRiskList { get; set; }
        public List<CreateBusinessContinuityPlanCriticalActivityCommand>? BusinessContinuityPlanCriticalActivityList { get; set; }
        public List<CreateBusinessContinuityPlanRelatedStaffCommand>? BusinessContinuityPlanRelatedStaffList { get; set; }
        public List<CreateBusinessContinuityPlanResponsibleCommand>? BusinessContinuityPlanResponsibleList { get; set; }
        public List<CreateBusinessContinuityPlanAssumptionCommand>? BusinessContinuityPlanAssumptionList { get; set; }
    }
}
