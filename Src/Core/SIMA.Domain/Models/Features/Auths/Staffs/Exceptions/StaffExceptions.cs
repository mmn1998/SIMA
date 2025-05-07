using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Exceptions;

public class StaffExceptions
{
    public static SimaResultException StaffNotSatisfiedException = new("400", "سمت و گروفایل انتخاب شده تکراری است!");
}
