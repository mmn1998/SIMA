using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;
public interface IIssuePriorityDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
