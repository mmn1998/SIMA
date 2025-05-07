using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Interfaces;

public interface IIssueApprovalRepository : IRepository<IssueApproval>
{
    Task<IssueApproval> GetById(long id);

}
