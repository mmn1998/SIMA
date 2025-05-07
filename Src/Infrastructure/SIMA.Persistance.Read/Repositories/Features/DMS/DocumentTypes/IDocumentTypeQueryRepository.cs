using SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.DocumentTypes;

public interface IDocumentTypeQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetDocumentTypeQueryResult>>> GetAll(GetAllDocumentTypesQuery request);
    Task<GetDocumentTypeQueryResult> GetById(long id);
}
