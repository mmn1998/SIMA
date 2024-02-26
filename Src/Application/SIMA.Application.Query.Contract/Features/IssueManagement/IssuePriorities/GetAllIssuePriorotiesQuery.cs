using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;

public class GetAllIssuePriorotiesQuery : IQuery<Result<List<GetIssuePriorotyQueryResult>>>
{
    public BaseRequest Request { get; set; } = new();
}