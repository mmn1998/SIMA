using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;

public interface IBusinessContinuityPlanDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BusinessContinuityPlanId? id = null);
    Task<bool> IsVersionUnique(string versionNumber, BusinessContinuityPlanId? id = null);
}
