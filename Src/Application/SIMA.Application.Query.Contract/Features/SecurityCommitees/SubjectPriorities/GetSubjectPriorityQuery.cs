using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;

public class GetSubjectPriorityQuery : IQuery<Result<GetSubjectPriorityQueryResult>>
{
    public long Id { get; set; }
}