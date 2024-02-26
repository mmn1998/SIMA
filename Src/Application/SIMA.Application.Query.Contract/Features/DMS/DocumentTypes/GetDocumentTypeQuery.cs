using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;

public class GetDocumentTypeQuery : IQuery<Result<GetDocumentTypeQueryResult>>
{
    public long Id { get; set; }
}
