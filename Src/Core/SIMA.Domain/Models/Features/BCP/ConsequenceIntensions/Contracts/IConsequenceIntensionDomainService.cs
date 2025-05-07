using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;

public interface IConsequenceIntensionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ConsequenceIntensionId? id = null);
}