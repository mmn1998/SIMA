using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.Frequencies.Contracts;

public interface IFrequencyDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, FrequencyId? id = null);
}