using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.DocumentExtensions;

public interface IDocumentExtensionQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetDocumentExtensionQueryResult>>> GetAll(GetAllDocumentExtensionsQuery request);
    Task<GetDocumentExtensionQueryResult> GetById(long id);
}
