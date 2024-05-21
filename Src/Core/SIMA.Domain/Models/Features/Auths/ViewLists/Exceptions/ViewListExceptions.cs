using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.Auths.ViewLists.Exceptions
{
    public class ViewListExceptions
    {
        public static SimaResultException NotSelectViewList = new("400", Messages.NotSelectViewList);
    }
}
