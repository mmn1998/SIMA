using SIMA.Application.Query.Contract.Features.Auths.UIInputElements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.UIInputElements;

public interface IUIInputElementQueryRepository : IQueryRepository
{
    Task<GetUIInputElementQueryResult> GetById(GetUIInputElementQuery request);
    Task<Result<IEnumerable<GetUIInputElementQueryResult>>> GetAll(GetAllUIInputElementsQuery request);
}