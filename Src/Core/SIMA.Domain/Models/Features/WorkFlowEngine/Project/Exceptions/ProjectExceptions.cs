using SIMA.Framework.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Exceptions
{
    public class ProjectExceptions
    {
        public static SimaResultException ProjectMemberIsManagerError = new("400", "فقط یک کاربر به عنوان مدیر پروژه میتواند در نظر گرفته شود  !");
    }
}
