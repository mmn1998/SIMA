using SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.ApprovalOptions
{
    public interface IApprovalOptionQueryRepository : IQueryRepository
    {
        Task<GetApprovalOptionQueryResult> FindById(long id);
        Task<Result<IEnumerable<GetApprovalOptionQueryResult>>> GetAll(GetAllApprovalOptionsQuery request);
    }
}
