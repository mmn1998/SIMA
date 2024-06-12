using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;

public class GetAllSubjectPrioritiesQuery : BaseRequest, IQuery<Result<IEnumerable<GetSubjectPriorityQueryResult>>>
{
}
