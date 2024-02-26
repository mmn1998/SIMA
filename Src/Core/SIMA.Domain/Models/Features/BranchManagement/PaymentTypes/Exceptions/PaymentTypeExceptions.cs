using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Exceptions;

public class PaymentTypeExceptions
{
    public static SimaResultException LengthPaymentTypeNameException = new("400", "عنوان نباید بیش از 200 کاراکتر باشد ");
}
