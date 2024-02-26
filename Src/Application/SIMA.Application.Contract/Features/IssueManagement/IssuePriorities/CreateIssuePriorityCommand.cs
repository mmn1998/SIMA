using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssuePriorities;

public class CreateIssuePriorityCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public float Ordering { get; set; }
}
