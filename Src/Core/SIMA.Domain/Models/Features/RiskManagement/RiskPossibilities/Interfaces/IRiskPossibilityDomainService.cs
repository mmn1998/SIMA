using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces
{
    public interface IRiskPossibilityDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
