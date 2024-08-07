using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface
{
    public interface IWorkFlowActorDomainService : IDomainService
    {
        Task<bool> IsAccessToEveryOne(long Id);
    }
}
