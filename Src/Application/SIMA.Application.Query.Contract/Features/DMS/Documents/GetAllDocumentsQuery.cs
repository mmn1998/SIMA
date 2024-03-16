using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.Documents;

public class GetAllDocumentsQuery : BaseRequest, IQuery<Result<List<GetAllDocumentQueryResult>>>
{
}
