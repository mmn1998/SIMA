using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;

public interface ISubjectPriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string Code, long Id);
}