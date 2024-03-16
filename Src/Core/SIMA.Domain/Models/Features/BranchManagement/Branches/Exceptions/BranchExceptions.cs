using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Exceptions;

public class BranchExceptions
{
    public static SimaResultException BranchDistanceException = new("400", "شعبه ها نباید نزدیک هم باشند!");
    public static SimaResultException ChiefAndDeputyAreSameException = new("400", "رئیس شعبه و معاون آن نمی تواند یک نفر باشند.");
    public static SimaResultException StaffCantHaveRoleInTwoBranchesException = new("400", "یک فرد نمی تواند به صورت همزمان در دو شعبه متفاوت سمت داشته باشند.");
    public static SimaResultException StaffShouldBeInSelectedException = new("400", "پرسنل خارج از دایره شهر انتخاب شده، نمی توانند به ریاست یا معاونت آن شعبه برگزیده شوند.");
}
