using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;

public interface IRiskDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RiskId? id = null);
}
