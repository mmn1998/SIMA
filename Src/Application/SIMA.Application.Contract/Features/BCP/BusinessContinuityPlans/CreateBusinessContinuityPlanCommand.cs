using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans
{
    public class CreateBusinessContinuityPlanCommand : ICommand<Result<long>>
    {
        public long? BusinessContinuityPlanId { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Scope { get; set; }
        public string? VersionNumber { get; set; }
        public string? ReleaseDate { get; set; }
        public List<CreateBusinessContinuityPlanStratgyCommand>? BusinessContinuityPlanStratgyList { get; set; }
        public List<CreateBusinessContinuityPlanServiceCommand>? BusinessContinuityPlanServiceList { get; set; }
        public List<CreateBusinessContinuityPlanRiskCommand>? BusinessContinuityPlanRiskList { get; set; }
        public List<CreateBusinessContinuityPlanCriticalActivityCommand>? BusinessContinuityPlanCriticalActivityList { get; set; }
        public List<CreateBusinessContinuityPlanRelatedStaffCommand>? BusinessContinuityPlanRelatedStaffList { get; set; }
        public List<CreateBusinessContinuityPlanResponsibleCommand>? BusinessContinuityPlanResponsibleList { get; set; }
        public List<CreateBusinessContinuityPlanAssumptionCommand>? BusinessContinuityPlanAssumptionList { get; set; }
    }
}
