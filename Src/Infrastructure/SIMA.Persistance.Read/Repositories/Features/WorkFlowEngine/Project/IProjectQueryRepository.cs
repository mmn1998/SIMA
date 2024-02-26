using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Project
{
    public interface IProjectQueryRepository : IQueryRepository
    {
        Task<GetProjectQueryResult> FindById(long id);
        Task<List<GetProjectQueryResult>> GetAll();
        Task<List<GetProjectQueryResult>> GetByDomainId(long domainId);
    }
}
