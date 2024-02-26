using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.Auths.Users.Exceptions;

public class UserExceptions
{
    public static SimaResultException PasswordNotValidException = new("400", "کلمه عبور معتبر نیست!");
    public static SimaResultException UsernameNotValidException = new("400", "نام کاربری معتبر نیست!");
    public static SimaResultException UsernameNotUniqueException = new("400", "نام کاربری تکراری است!");
}
