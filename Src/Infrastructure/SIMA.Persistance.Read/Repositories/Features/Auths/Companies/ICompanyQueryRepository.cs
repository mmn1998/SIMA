using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Companies;

public interface ICompanyQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetCompanyQueryResult>>> GetAll(GetAllCompanyQuery? request = null);
    Task<GetCompanyQueryResult> FindById(long id);
}
