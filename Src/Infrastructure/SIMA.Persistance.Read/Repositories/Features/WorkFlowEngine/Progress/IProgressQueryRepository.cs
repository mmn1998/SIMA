using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Progress
{
    public interface IProgressQueryRepository : IQueryRepository
    {
        Task<GetProgressQueryResult> FindById(long id);
        Task<Result<IEnumerable<GetProgressQueryResult>>> GetAll(GetAllProgressQuery request);
    }
}
