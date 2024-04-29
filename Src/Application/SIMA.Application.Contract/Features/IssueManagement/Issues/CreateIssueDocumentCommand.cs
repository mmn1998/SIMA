using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class CreateIssueDocumentCommand : ICommand<Result<long>>
    {
        public long? DocumentId { get; set; }
    }
}
