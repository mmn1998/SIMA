using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;

public class GetIssueTypesQuery : IQuery<Result<GetIssueTypesQueryResult>>
{
    public long Id { get; set; }
}
