using Sima.Framework.Core.Mediator;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Services.BehsazanServices.Request
{
    public class TrustCurrencyDraft :ICommand<Result<GetTrustyDraftInqueryQueryResult>>
    {
        public string draftId { get; set; }
    }
}
