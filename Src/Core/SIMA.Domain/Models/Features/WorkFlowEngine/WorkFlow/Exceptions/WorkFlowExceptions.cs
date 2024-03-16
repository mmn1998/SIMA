using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Exceptions
{
    public class WorkFlowExceptions
    {
        public static SimaResultException WorkFlowNameRequiredException = new("400", "نام فرایند اجباری است !");
        public static SimaResultException WorkFlowCodeIsUniqueException = new("400", "کد فرایند نمی تواند تکراری باشد  !");
    }
}
