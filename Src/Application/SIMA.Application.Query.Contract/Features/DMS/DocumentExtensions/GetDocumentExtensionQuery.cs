using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;

public class GetDocumentExtensionQuery : IQuery<Result<GetDocumentExtensionQueryResult>>
{
    public long Id { get; set; }
}
