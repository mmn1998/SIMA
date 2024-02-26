using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Interfaces;
public interface IIssueApprovalDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
