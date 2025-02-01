using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;

public interface IRiskLevelCobitDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RiskLevelCobitId? id = null);
}