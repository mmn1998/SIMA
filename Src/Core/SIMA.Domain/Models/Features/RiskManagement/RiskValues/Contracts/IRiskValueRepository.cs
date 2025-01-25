using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;

public interface IRiskValueRepository : IRepository<RiskValue>
{
    Task<RiskValue> GetById(RiskValueId id);
}