using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.Consequences.Contracts;

public interface IConsequenceDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConsequenceId? id = null);
}