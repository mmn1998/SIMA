using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface
{
    public interface IApprovalOptionDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
