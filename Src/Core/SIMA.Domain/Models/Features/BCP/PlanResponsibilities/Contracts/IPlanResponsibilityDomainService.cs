using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts
{
    public interface IPlanResponsibilityDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, PlanResponsibilityId? id = null);

    }
}
