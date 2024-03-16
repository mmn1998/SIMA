using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;

public class GetAllDocumentTypesQuery : BaseRequest, IQuery<Result<List<GetDocumentTypeQueryResult>>>
{
}