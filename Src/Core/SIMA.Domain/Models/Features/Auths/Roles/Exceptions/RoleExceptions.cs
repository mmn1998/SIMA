using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.Auths.Roles.Exceptions;

public class RoleExceptions
{
    public static SimaResultException RoleNotSatisfiedException = new("400", "ترکیب کد و نام انگلیسی باید یکتا باشد!");
}
