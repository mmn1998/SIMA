using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;

public class GetIssuePriorotyQuery : IQuery<Result<GetIssuePriorotyQueryResult>>
{
    public long Id { get; set; }
}
