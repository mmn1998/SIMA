using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions
{
    public class ModifyWorkFlowDocumentExtentionCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long WorkflowId { get; set; }
        public long DocumentExtensionId { get; set; }
        public long ActiveStatusId { get; set; }
    }
}
