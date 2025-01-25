using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.Frequencies.Contracts;

public interface IFrequencyRepository : IRepository<Frequency>
{
    Task<Frequency> GetById(FrequencyId id);
}