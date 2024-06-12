using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces
{
    public interface IRiskTypeDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
