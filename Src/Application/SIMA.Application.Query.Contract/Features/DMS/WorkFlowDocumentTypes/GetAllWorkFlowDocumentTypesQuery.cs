using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;

public class GetAllWorkFlowDocumentTypesQuery : IQuery<Result<List<GetWorkFlowDocumentTypeQueryResult>>>
{
    public BaseRequest Request { get; set; } = new();
}