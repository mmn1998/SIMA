using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface
{
    public interface IApprovalOptionRepository : IRepository<ApprovalOption>
    {
        Task<ApprovalOption> GetById(long id);
    }
}
