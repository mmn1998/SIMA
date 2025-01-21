using SIMA.Application.Services.BehsazanServices.Request;
using SIMA.Application.Services.BehsazanServices.Response;

namespace SIMA.Application.Services.BehsazanServices
{
    public interface IBehsazanService
    {
        Task<GetTrustCurrencyDraftMansha> GetTrustyDraft(TrustCurrencyDraft request);
    }
}
