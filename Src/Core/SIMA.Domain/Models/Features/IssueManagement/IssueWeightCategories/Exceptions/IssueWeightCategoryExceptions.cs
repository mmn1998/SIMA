using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Exceptions
{
    public class IssueWeightCategoryExceptions
    {
        public static SimaResultException IssueWeightCategoryNotFoundException = new("400", "وزن وارد شده اشتباه می باشد!");

    }
}
