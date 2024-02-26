using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface
{
    public interface IWorkFlowDomainService : IDomainService
    {
        Task<bool> SteteIsCodeUnique(string code, long id);
        Task<bool> CheckWorkFlow(long workflowId);
    }
}
