using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues;

public class CreateIssueCommentCommand : ICommand<Result<long>>
{
    public long IssueId { get; set; }
    public string Comment { get; set; }
}
