using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;

public interface IInquiryResponseDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, InquiryResponseId? id = null);
    Task<bool> CurrencyTypeIdEquals(long wageRateId, long inquiryRequestCurrencyId);
}
