using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Exeptions
{
    public class ProgressExceptions
    {
        public static SimaResultException ProgressNameRequiredException = new("400", "نام جریان اجباری است !");

    }
}
