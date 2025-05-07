using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;

public interface ISeverityRepository : IRepository<Severity>
{
    Task<Severity> GetById(SeverityId id);
}