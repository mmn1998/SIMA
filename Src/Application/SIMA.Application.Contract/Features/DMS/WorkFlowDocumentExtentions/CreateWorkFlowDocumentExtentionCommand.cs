using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions
{
    public class CreateWorkFlowDocumentExtentionCommand : ICommand<Result<long>>
    {

        public long WorkflowId { get; set; }
        public long DocumentExtensionId { get; set; }
    }
}
