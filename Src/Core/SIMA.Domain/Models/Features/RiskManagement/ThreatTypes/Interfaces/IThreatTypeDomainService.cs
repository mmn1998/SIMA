using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces
{
    public interface IThreatTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
