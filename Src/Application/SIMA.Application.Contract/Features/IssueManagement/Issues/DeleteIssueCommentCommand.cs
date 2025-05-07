using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues;

public class DeleteIssueCommentCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long IssueId { get; set; }
}
