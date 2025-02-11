using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;

public class GetSolutionPeriorityQuery : IQuery<Result<GetSolutionPeriorityQueryResult>>
{
    public long Id { get; set; }
}