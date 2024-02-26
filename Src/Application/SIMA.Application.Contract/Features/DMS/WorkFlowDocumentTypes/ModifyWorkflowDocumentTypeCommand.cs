using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.DMS.WorkFlowDocumentTypes;

public class ModifyWorkflowDocumentTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long WorkflowId { get; set; }
    public long DocumentTypeId { get; set; }
    public long ActiveStatusId { get; set; }
}
