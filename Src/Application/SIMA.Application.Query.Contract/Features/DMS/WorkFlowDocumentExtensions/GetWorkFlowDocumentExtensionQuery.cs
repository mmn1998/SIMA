using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions
{
    public class GetWorkFlowDocumentExtensionQuery : IQuery<Result<GetWorkFlowDocumentExtensionQueryResult>>
    {
        public long Id { get; set; }
    }
}
