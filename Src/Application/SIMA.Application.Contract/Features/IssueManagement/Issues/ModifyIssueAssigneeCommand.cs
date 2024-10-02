using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class ModifyIssueAssigneeCommand : ICommand<Result<long>>
    {
        public long IssueId { get; set; }
        public long UserId { get; set; }
    }
}
