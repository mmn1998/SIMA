using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssuePriorities;

public class DeleteIssuePriorityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
