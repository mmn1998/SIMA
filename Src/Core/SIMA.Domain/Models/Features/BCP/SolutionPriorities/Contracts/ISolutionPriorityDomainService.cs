using SIMA.Domain.Models.Features.BCP.SolutionPriorities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;

public interface ISolutionPriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SolutionPriorityId? id = null);
}