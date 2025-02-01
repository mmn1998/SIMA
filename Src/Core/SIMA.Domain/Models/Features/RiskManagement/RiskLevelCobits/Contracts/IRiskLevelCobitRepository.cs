using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;

public interface IRiskLevelCobitRepository : IRepository<RiskLevelCobit>
{
    Task<RiskLevelCobit> GetById(RiskLevelCobitId id);
}
