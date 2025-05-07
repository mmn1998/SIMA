using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Project
{
    public interface IProjectQueryRepository : IQueryRepository
    {
        Task<GetProjectQueryResult> FindById(long id);
        Task<Result<IEnumerable<GetProjectQueryResult>>> GetAll(GetAllProjectsQuery request);
        Task<List<GetProjectQueryResult>> GetByDomainId(long domainId);
    }
}
