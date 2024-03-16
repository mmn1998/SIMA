using SIMA.Framework.Common.Exceptions;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions
{
    public class IssueExceptions
    {
        public static SimaResultException IssueWeightErrorException = new("400", "وزن وارد شده اشتباه می باشد!");
        public static SimaResultException IssueErrorException = new("400", "خطا در ثبت مورد!");
        public static SimaResultException IssueCodeIsNotUnique = new("400", "کد مورد یکتا نمی باشد");
        public static SimaResultException IssueDueDateError = new("400", "تاریخ تحویل اشتباه می باشد");
        public static SimaResultException CreateIssueWithChechActorException = new("400", "شما دسترسی ایجاد مورد برای این فرایند را ندارید");
    }
}
