using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftIssueTypes;

public class GetAllDraftIssueTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetDraftIssueTypeQueryResult>>>
{
}