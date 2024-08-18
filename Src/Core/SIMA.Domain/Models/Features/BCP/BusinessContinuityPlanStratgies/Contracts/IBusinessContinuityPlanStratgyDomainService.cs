using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Contracts
{
    public interface IBusinessContinuityPlanStratgyDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, BusinessContinuityPlanStratgyId? id = null);
    }
}
