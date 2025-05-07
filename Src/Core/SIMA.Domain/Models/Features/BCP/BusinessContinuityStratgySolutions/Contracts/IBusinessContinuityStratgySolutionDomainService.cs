using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Contracts
{
    public interface IBusinessContinuityStratgySolutionDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, BusinessContinuityStratgySolutionId? id = null);
    }
}
