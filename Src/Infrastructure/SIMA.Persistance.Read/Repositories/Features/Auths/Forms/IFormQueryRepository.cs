using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Forms;

public interface IFormQueryRepository : IQueryRepository
{
    Task<GetFormQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetFormQueryResult>>> GetAll(GetAllFormQuery? baseRequests = null);
    Task<List<GetViewResult>> FetchFromView(string viewName);
}
