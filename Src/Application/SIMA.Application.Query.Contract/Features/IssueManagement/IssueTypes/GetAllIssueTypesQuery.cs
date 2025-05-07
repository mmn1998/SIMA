using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;

public class GetAllIssueTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetIssueTypesQueryResult>>>
{
}