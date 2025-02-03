using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;

public interface IRiskLevelCobitRepository : IRepository<RiskLevelCobit>
{
    Task<RiskLevelCobit> GetById(RiskLevelCobitId id);
}