using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Contracts;

public interface ISolutionPeriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SolutionPeriorityId? id = null);
}