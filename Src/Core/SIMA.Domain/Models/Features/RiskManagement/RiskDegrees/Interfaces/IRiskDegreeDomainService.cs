using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces
{
    public interface IRiskDegreeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
        bool IsHexCodeValid(string hexCode);
    }
}
