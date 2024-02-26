using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class CreateIssueLinkCommand : ICommand<Result<long>>
    {
        public long IssueIdLinkedTo { get; set; }
        public long IssueLinkReasonTo { get; set; }
    }
}
