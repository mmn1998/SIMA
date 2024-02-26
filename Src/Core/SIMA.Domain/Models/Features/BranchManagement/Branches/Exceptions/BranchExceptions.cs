using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Exceptions;

public class BranchExceptions
{
    public static SimaResultException BranchDistanceException = new("400", "شعبه ها نباید نزدیک هم باشند!");
}
