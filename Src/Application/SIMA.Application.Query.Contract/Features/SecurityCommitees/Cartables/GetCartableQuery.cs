using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;

public class GetCartableQuery : IQuery<Result<GetCartableQueryResult>>
{
    public long MeetingId { get; set; }
    public long IssueId { get; set; }
}
