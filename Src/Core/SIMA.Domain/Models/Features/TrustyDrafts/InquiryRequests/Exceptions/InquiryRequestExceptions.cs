using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Exceptions;

public static class InquiryRequestExceptions
{
    public static SimaResultException DepositProformaCurrencyTypeIdException = new(CodeMessges._400Code, Messages.DepositProformaCurrencyTypeIdException);
}
