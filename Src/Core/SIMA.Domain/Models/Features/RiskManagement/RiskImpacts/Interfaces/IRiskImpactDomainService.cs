using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces
{
    public interface IRiskImpactDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
