using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;

public interface IRiskRepository : IRepository<Risk>
{
    Task<Risk> GetById(RiskId id);
    Task<Risk?> GetLast();
}