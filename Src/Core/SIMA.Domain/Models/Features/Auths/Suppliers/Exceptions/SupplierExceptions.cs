using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Exceptions;

public static class SupplierExceptions
{
    public static SimaResultException NationalCodeException = new SimaResultException(code: CodeMessges._100072Code, message: Messages.NationalCodeIsInvalidError);
    public static SimaResultException NationalIdException = new SimaResultException(code: CodeMessges._100072Code, message: CodeMessges.NationalIdError);
}
