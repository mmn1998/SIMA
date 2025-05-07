using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface
{
    public interface IProjectDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
