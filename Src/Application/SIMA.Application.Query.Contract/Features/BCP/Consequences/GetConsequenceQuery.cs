using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.Consequences;

public class GetConsequenceQuery : IQuery<Result<GetConsequenceQueryResult>>
{
    public long Id { get; set; }
}