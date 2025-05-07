using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;

public class GetSolutionPriorityQuery : IQuery<Result<GetSolutionPriorityQueryResult>>
{
    public long Id { get; set; }
}