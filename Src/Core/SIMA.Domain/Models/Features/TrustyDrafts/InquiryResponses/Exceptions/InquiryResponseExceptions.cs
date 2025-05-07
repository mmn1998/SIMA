using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Exceptions;

public class InquiryResponseExceptions
{
    public static SimaResultException InvalidWageRateException = new SimaResultException(CodeMessges._1001001Code, Messages.InvalidWageRateException);
}
