using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;

public class GetConsequenceIntensionQuery : IQuery<Result<GetConsequenceIntensionQueryResult>>
{
    public long Id { get; set; }
}