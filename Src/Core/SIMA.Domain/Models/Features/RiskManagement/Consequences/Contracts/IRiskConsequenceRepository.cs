using SIMA.Domain.Models.Features.RiskManagement.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.Consequences.Contracts;

public interface IRiskConsequenceRepository : IRepository<RiskConsequence>
{
    Task<RiskConsequence> GetById(RiskConsequenceId id);
}