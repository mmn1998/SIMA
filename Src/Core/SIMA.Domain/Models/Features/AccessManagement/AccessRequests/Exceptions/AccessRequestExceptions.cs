using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Exceptions;

public static class AccessRequestExceptions
{
    public static SimaResultException DateOnlyException = new SimaResultException(CodeMessges._100069Code, Messages.DateOnlyError);
    public static SimaResultException TimeOnlyException = new SimaResultException(CodeMessges._100071Code, Messages.TimeOnlyError);
}
