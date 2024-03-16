using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowCompany;

public interface IWorkFlowCompanyQueryRepository : IQueryRepository
{
    Task<GetWorkFlowCompanyQueryResult> FindById(long id);
    Task<Result<List<GetWorkFlowCompanyQueryResult>>> GetAll(GetAllWorkFlowCompanyQuery request);
}
