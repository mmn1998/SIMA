using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces
{
    public interface IRiskLevelDomainService :IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
