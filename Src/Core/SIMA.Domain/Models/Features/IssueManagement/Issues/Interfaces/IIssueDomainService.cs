using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
public interface IIssueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<bool> CheckDueDate(DateTime dueDate);
}
