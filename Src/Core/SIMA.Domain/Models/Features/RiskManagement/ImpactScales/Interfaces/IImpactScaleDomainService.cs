using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Interfaces
{
    public interface IImpactScaleDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
