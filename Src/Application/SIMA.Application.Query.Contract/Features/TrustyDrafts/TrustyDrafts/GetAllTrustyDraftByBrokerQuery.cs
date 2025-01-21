using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts
{
    public class GetAllTrustyDraftByBrokerQuery : IQuery<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>
    {
        public long? BrokerId { get; set; }
    }
}
