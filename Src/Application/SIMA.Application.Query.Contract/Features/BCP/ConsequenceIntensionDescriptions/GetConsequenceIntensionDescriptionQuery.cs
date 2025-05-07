using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;

public class GetConsequenceIntensionDescriptionQuery : IQuery<Result<GetConsequenceIntensionDescriptionQueryResult>>
{
    public long Id { get; set; }
}