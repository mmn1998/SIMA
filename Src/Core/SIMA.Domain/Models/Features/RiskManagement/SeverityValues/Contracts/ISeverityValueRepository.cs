using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;

public interface ISeverityValueRepository : IRepository<SeverityValue>
{
    Task<SeverityValue> GetById(SeverityValueId id);
}