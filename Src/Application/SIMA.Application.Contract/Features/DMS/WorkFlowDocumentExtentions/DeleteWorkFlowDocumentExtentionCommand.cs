using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions
{
    public class DeleteWorkFlowDocumentExtentionCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
