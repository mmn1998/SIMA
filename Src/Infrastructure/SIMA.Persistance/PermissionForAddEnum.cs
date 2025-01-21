using System.ComponentModel.DataAnnotations;

namespace SIMA.Persistance
{
    public enum PermissionForAddEnum : int
    {
        #region AgentBankWageShareStatuses(2331-2340)
        [Display(GroupName = "InquiryRequests", Name = "GetAll", Description = "نمایش فهرست درخواست استعلام ")]
        AgentBankWageShareStatusGetAll = 2331,

        [Display(GroupName = "InquiryRequests", Name = "Get", Description = "نمایش جزئیات درخواست استعلام")]
        AgentBankWageShareStatusGet = 2332,

        [Display(GroupName = "InquiryRequests", Name = "Post", Description = "ثبت  نهایی درخواست استعلام")]
        AgentBankWageShareStatusPost = 2333,

        [Display(GroupName = "InquiryRequests", Name = "Put", Description = "بروزرسانی درخواست استعلام")]
        AgentBankWageShareStatusPut = 2334,

        [Display(GroupName = "InquiryRequests", Name = "Delete", Description = "حذف درخواست استعلام")]
        AgentBankWageShareStatusDelete = 2335,
        #endregion
    }
}
