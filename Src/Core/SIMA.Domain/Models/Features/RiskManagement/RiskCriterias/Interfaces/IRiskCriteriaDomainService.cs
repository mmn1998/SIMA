using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces
{
    public interface IRiskCriteriaDomainService :IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
