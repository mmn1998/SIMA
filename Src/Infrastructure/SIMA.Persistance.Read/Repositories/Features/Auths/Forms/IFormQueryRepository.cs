using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Forms;

public interface IFormQueryRepository : IQueryRepository
{
    Task<GetFormQueryResult> FindById(long id);
    Task<Result<IEnumerable<GetFormQueryResult>>> GetAll(GetAllFormQuery requests);
    Task<Result<IEnumerable<GetFormFieldsQueryResult>>> GetAllFormFields(GetAllFormFieldsQuery request);
    Task<List<GetViewResult>> FetchFromView(string viewName);
    Task<Result<IEnumerable<GetFormQueryResult>>> GetFormByDomainId(long domainId);
}
