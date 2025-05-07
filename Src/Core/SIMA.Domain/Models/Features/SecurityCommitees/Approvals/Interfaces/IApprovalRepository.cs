using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Interfaces;

public interface IApprovalRepository : IRepository<Approval>
{
    Task<Approval> GetById(long Id);
}
