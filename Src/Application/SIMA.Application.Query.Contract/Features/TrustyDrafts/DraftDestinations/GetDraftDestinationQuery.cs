using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations
{
    public class GetDraftDestinationQuery : IQuery<Result<GetDraftDestinationQueryResult>>
    {
        public long Id { get; set; }
    }
}
