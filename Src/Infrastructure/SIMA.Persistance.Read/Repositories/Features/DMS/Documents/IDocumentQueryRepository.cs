using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.DMS.Documents;

public interface IDocumentQueryRepository : IQueryRepository
{
    Task<GetDocumentResult> GetForDownload(long documentId);
    Task<List<GetAllDocumentQueryResult>> GetAll(BaseRequest baseRequest);
}
