using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Contracts
{
    public interface IBusinessContinuityPlanVersioningDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, BusinessContinuityPlanVersioningId? id = null);
    }
}
