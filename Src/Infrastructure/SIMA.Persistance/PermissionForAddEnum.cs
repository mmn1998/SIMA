using System.ComponentModel.DataAnnotations;

namespace SIMA.Persistance
{
    public enum PermissionForAddEnum : int
    {
        #region ServiceType(1016-1020)

        [Display(GroupName = "ServiceType", Name = "GetAll", Description = "نمایش فهرست نوع خدمت")]
        ServiceTypeGetAll = 1016,
        [Display(GroupName = "ServiceType", Name = "Get", Description = "نمایش جزئیات نوع خدمت ")]
        ServiceTypeGet = 1017,
        [Display(GroupName = "ServiceType", Name = "Post", Description = "ثبت نوع خدمت ")]
        ServiceTypePost = 1018,
        [Display(GroupName = "ServiceType", Name = "Put", Description = "بروزرسانی نوع خدمت ")]
        ServiceTypePut = 1019,
        [Display(GroupName = "ServiceType", Name = "Delete", Description = "حذف نوع خدمت")]
        ServiceTypeDelete = 1020,
        #endregion



        #region Risk-Cobit(3000-3500)
        #region AffectedHistory(3000-3004)

        [Display(GroupName = "AffectedHistory", Name = "GetAll", Description = "نمایش فهرست سابقه متاثر شدن کسب و کار")]
        AffectedHistoryGetAll = 3000,
        [Display(GroupName = "AffectedHistory", Name = "Get", Description = "نمایش جزئیات سابقه متاثر شدن کسب و کار")]
        AffectedHistoryGet = 3001,
        [Display(GroupName = "AffectedHistory", Name = "Post", Description = "ثبت سابقه متاثر شدن کسب و کار")]
        AffectedHistoryPost = 3002,
        [Display(GroupName = "AffectedHistory", Name = "Put", Description = "بروزرسانی سابقه متاثر شدن کسب و کار")]
        AffectedHistoryPut = 3003,
        [Display(GroupName = "AffectedHistory", Name = "Delete", Description = "حذف سابقه متاثر شدن کسب و کار")]
        AffectedHistoryDelete = 3004,
        #endregion
        #region CobitCategory(3005-3009)

        [Display(GroupName = "CobitCategory", Name = "GetAll", Description = "نمایش فهرست دسته بندی کوبیت")]
        CobitCategoryGetAll = 3005,
        [Display(GroupName = "CobitCategory", Name = "Get", Description = "نمایش جزئیات دسته بندی کوبیت")]
        CobitCategoryGet = 3006,
        [Display(GroupName = "CobitCategory", Name = "Post", Description = "ثبت دسته بندی کوبیت")]
        CobitCategoryPost = 3007,
        [Display(GroupName = "CobitCategory", Name = "Put", Description = "بروزرسانی دسته بندی کوبیت")]
        CobitCategoryPut = 3008,
        [Display(GroupName = "CobitCategory", Name = "Delete", Description = "حذف دسته بندی کوبیت")]
        CobitCategoryDelete = 3009,
        #endregion
        #region CobitScenario(3010-3014)

        [Display(GroupName = "CobitScenario", Name = "GetAll", Description = "نمایش فهرست سناریو کوبیت ")]
        CobitScenarioGetAll = 3010,
        [Display(GroupName = "CobitScenario", Name = "Get", Description = "نمایش جزئیات سناریو کوبیت ")]
        CobitScenarioGet = 3011,
        [Display(GroupName = "CobitScenario", Name = "Post", Description = "ثبت سناریو کوبیت ")]
        CobitScenarioPost = 3012,
        [Display(GroupName = "CobitScenario", Name = "Put", Description = "بروزرسانی سناریو کوبیت ")]
        CobitScenarioPut = 3013,
        [Display(GroupName = "CobitScenario", Name = "Delete", Description = "حذف سناریو کوبیت")]
        CobitScenarioDelete = 3014,
        #endregion
        #region ConsequenceCategory(3015-3019)

        [Display(GroupName = "ConsequenceCategory", Name = "GetAll", Description = "نمایش فهرست دسته بندی های پیامد ")]
        ConsequenceCategoryGetAll = 3015,
        [Display(GroupName = "ConsequenceCategory", Name = "Get", Description = "نمایش جزئیات دسته بندی های پیامد ")]
        ConsequenceCategoryGet = 3016,
        [Display(GroupName = "ConsequenceCategory", Name = "Post", Description = "ثبت دسته بندی های پیامد ")]
        ConsequenceCategoryPost = 3017,
        [Display(GroupName = "ConsequenceCategory", Name = "Put", Description = "بروزرسانی دسته بندی های پیامد ")]
        ConsequenceCategoryPut = 3018,
        [Display(GroupName = "ConsequenceCategory", Name = "Delete", Description = "حذف دسته بندی های پیامد")]
        ConsequenceCategoryDelete = 3019,
        #endregion
        #region ConsequenceLevel(3020-3021)

        [Display(GroupName = "ConsequenceLevel", Name = "GetAll", Description = "نمایش فهرست سطح پیامد ")]
        ConsequenceLevelGetAll = 3020,
        [Display(GroupName = "ConsequenceLevel", Name = "Get", Description = "نمایش جزئیات سطح پیامد ")]
        ConsequenceLevelGet = 3021,
        [Display(GroupName = "ConsequenceLevel", Name = "Post", Description = "ثبت سطح پیامد ")]
        ConsequenceLevelPost = 3022,
        [Display(GroupName = "ConsequenceLevel", Name = "Put", Description = "بروزرسانی سطح پیامد ")]
        ConsequenceLevelPut = 3023,
        [Display(GroupName = "ConsequenceLevel", Name = "Delete", Description = "حذف سطح پیامد")]
        ConsequenceLevelDelete = 3024,
        #endregion
        #region CurrentOccurrenceProbability(3025-3029)

        [Display(GroupName = "CurrentOccurrenceProbability", Name = "GetAll", Description = "نمایش فهرست احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityGetAll = 3025,
        [Display(GroupName = "CurrentOccurrenceProbability", Name = "Get", Description = "نمایش جزئیات احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityGet = 3026,
        [Display(GroupName = "CurrentOccurrenceProbability", Name = "Post", Description = "ثبت احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityPost = 3027,
        [Display(GroupName = "CurrentOccurrenceProbability", Name = "Put", Description = "بروزرسانی احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityPut = 3028,
        [Display(GroupName = "CurrentOccurrenceProbability", Name = "Delete", Description = "حذف احتمال وقوع فعلی")]
        CurrentOccurrenceProbabilityDelete = 3029,
        #endregion
        #region CurrentOccurrenceProbabilityValue(3030-3034)

        [Display(GroupName = "CurrentOccurrenceProbabilityValue", Name = "GetAll", Description = "نمایش فهرست ارزش احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityValueGetAll = 3030,
        [Display(GroupName = "CurrentOccurrenceProbabilityValue", Name = "Get", Description = "نمایش جزئیات ارزش احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityValueGet = 3031,
        [Display(GroupName = "CurrentOccurrenceProbabilityValue", Name = "Post", Description = "ثبت ارزش احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityValuePost = 3032,
        [Display(GroupName = "CurrentOccurrenceProbabilityValue", Name = "Put", Description = "بروزرسانی ارزش احتمال وقوع فعلی ")]
        CurrentOccurrenceProbabilityValuePut = 3033,
        [Display(GroupName = "CurrentOccurrenceProbabilityValue", Name = "Delete", Description = "حذف ارزش احتمال وقوع فعلی")]
        CurrentOccurrenceProbabilityValueDelete = 3034,
        #endregion
        #region Frequency(3035-3039)

        [Display(GroupName = "Frequency", Name = "GetAll", Description = "نمایش فهرست فراوانی ")]
        FrequencyGetAll = 3035,
        [Display(GroupName = "Frequency", Name = "Get", Description = "نمایش جزئیات فراوانی ")]
        FrequencyGet = 3036,
        [Display(GroupName = "Frequency", Name = "Post", Description = "ثبت فراوانی ")]
        FrequencyPost = 3037,
        [Display(GroupName = "Frequency", Name = "Put", Description = "بروزرسانی فراوانی ")]
        FrequencyPut = 3038,
        [Display(GroupName = "Frequency", Name = "Delete", Description = "حذف فراوانی")]
        FrequencyDelete = 3039,
        #endregion
        #region InherentOccurrenceProbability(3040-3044)

        [Display(GroupName = "InherentOccurrenceProbability", Name = "GetAll", Description = "نمایش فهرست ماتریس احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityGetAll = 3040,
        [Display(GroupName = "InherentOccurrenceProbability", Name = "Get", Description = "نمایش جزئیات ماتریس احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityGet = 3041,
        [Display(GroupName = "InherentOccurrenceProbability", Name = "Post", Description = "ثبت ماتریس احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityPost = 3042,
        [Display(GroupName = "InherentOccurrenceProbability", Name = "Put", Description = "بروزرسانی ماتریس احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityPut = 3043,
        [Display(GroupName = "InherentOccurrenceProbability", Name = "Delete", Description = "حذف ماتریس احتمال وقوع ذاتی")]
        InherentOccurrenceProbabilityDelete = 3044,
        #endregion
        #region InherentOccurrenceProbabilityValue(3045-3049)

        [Display(GroupName = "InherentOccurrenceProbabilityValue", Name = "GetAll", Description = "نمایش فهرست ارزش احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityValueGetAll = 3045,
        [Display(GroupName = "InherentOccurrenceProbabilityValue", Name = "Get", Description = "نمایش جزئیات ارزش احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityValueGet = 3046,
        [Display(GroupName = "InherentOccurrenceProbabilityValue", Name = "Post", Description = "ثبت ارزش احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityValuePost = 3047,
        [Display(GroupName = "InherentOccurrenceProbabilityValue", Name = "Put", Description = "بروزرسانی ارزش احتمال وقوع ذاتی ")]
        InherentOccurrenceProbabilityValuePut = 3048,
        [Display(GroupName = "InherentOccurrenceProbabilityValue", Name = "Delete", Description = "حذف ارزش احتمال وقوع ذاتی")]
        InherentOccurrenceProbabilityValueDelete = 3049,
        #endregion
        #region MatrixsAValue(3050-3054)

        [Display(GroupName = "MatrixAValue", Name = "GetAll", Description = "نمایش فهرست ارزش ماتریس")]
        MatrixAValueGetAll = 3050,
        [Display(GroupName = "MatrixAValue", Name = "Get", Description = "نمایش جزئیات ارزش ماتریس ")]
        MatrixAValueGet = 3051,
        [Display(GroupName = "MatrixAValue", Name = "Post", Description = "ثبت ارزش ماتریس ")]
        MatrixAValuePost = 3052,
        [Display(GroupName = "MatrixAValue", Name = "Put", Description = "بروزرسانی ارزش ماتریس ")]
        MatrixAValuePut = 3053,
        [Display(GroupName = "MatrixAValue", Name = "Delete", Description = "حذف ارزش ماتریس")]
        MatrixAValueDelete = 3054,
        #endregion
        #region MatrixsA(3055-3059)

        [Display(GroupName = "MatrixA", Name = "GetAll", Description = "نمایش فهرست ماتریس")]
        MatrixAGetAll = 3055,
        [Display(GroupName = "MatrixA", Name = "Get", Description = "نمایش جزئیات ماتریس ")]
        MatrixAGet = 3056,
        [Display(GroupName = "MatrixA", Name = "Post", Description = "ثبت ماتریس ")]
        MatrixAPost = 3057,
        [Display(GroupName = "MatrixA", Name = "Put", Description = "بروزرسانی ماتریس ")]
        MatrixAPut = 3058,
        [Display(GroupName = "MatrixA", Name = "Delete", Description = "حذف ماتریس")]
        MatrixADelete = 3059,
        #endregion
        #region RiskLevelCobit(3060-3064)

        [Display(GroupName = "RiskLevelCobit", Name = "GetAll", Description = "نمایش فهرست سطح ریسک کوبیت")]
        RiskLevelCobitGetAll = 3060,
        [Display(GroupName = "RiskLevelCobit", Name = "Get", Description = "نمایش جزئیات سطح ریسک کوبیت ")]
        RiskLevelCobitGet = 3061,
        [Display(GroupName = "RiskLevelCobit", Name = "Post", Description = "ثبت سطح ریسک کوبیت ")]
        RiskLevelCobitPost = 3062,
        [Display(GroupName = "RiskLevelCobit", Name = "Put", Description = "بروزرسانی سطح ریسک کوبیت ")]
        RiskLevelCobitPut = 3063,
        [Display(GroupName = "RiskLevelCobit", Name = "Delete", Description = "حذف سطح ریسک کوبیت")]
        RiskLevelCobitDelete = 3064,
        #endregion
        #region RiskValue(3065-3069)

        [Display(GroupName = "RiskValue", Name = "GetAll", Description = "نمایش فهرست ارزش ریسک")]
        RiskValueGetAll = 3065,
        [Display(GroupName = "RiskValue", Name = "Get", Description = "نمایش جزئیات ارزش ریسک ")]
        RiskValueGet = 3066,
        [Display(GroupName = "RiskValue", Name = "Post", Description = "ثبت ارزش ریسک ")]
        RiskValuePost = 3067,
        [Display(GroupName = "RiskValue", Name = "Put", Description = "بروزرسانی ارزش ریسک ")]
        RiskValuePut = 3068,
        [Display(GroupName = "RiskValue", Name = "Delete", Description = "حذف ارزش ریسک")]
        RiskValueDelete = 3069,
        #endregion
        #region ScenarioHistory(3070-3074)

        [Display(GroupName = "ScenarioHistory", Name = "GetAll", Description = "نمایش فهرست سابقه سناریو ریسک")]
        ScenarioHistoryGetAll = 3070,
        [Display(GroupName = "ScenarioHistory", Name = "Get", Description = "نمایش جزئیات سابقه سناریو ریسک ")]
        ScenarioHistoryGet = 3071,
        [Display(GroupName = "ScenarioHistory", Name = "Post", Description = "ثبت سابقه سناریو ریسک ")]
        ScenarioHistoryPost = 3072,
        [Display(GroupName = "ScenarioHistory", Name = "Put", Description = "بروزرسانی سابقه سناریو ریسک ")]
        ScenarioHistoryPut = 3073,
        [Display(GroupName = "ScenarioHistory", Name = "Delete", Description = "حذف سابقه سناریو ریسک")]
        ScenarioHistoryDelete = 3074,
        #endregion
        #region Severity(3075-3079)

        [Display(GroupName = "Severity", Name = "GetAll", Description = "نمایش فهرست شدت اثر")]
        SeverityGetAll = 3075,
        [Display(GroupName = "Severity", Name = "Get", Description = "نمایش جزئیات شدت اثر ")]
        SeverityGet = 3076,
        [Display(GroupName = "Severity", Name = "Post", Description = "ثبت شدت اثر ")]
        SeverityPost = 3077,
        [Display(GroupName = "Severity", Name = "Put", Description = "بروزرسانی شدت اثر ")]
        SeverityPut = 3078,
        [Display(GroupName = "Severity", Name = "Delete", Description = "حذف شدت اثر")]
        SeverityDelete = 3079,
        #endregion
        #region SeverityValue(3080-3084)

        [Display(GroupName = "SeverityValue", Name = "GetAll", Description = "نمایش فهرست ارزش شدت اثر")]
        SeverityValueGetAll = 3080,
        [Display(GroupName = "SeverityValue", Name = "Get", Description = "نمایش جزئیات ارزش شدت اثر ")]
        SeverityValueGet = 3081,
        [Display(GroupName = "SeverityValue", Name = "Post", Description = "ثبت ارزش شدت اثر ")]
        SeverityValuePost = 3082,
        [Display(GroupName = "SeverityValue", Name = "Put", Description = "بروزرسانی ارزش شدت اثر ")]
        SeverityValuePut = 3083,
        [Display(GroupName = "SeverityValue", Name = "Delete", Description = "حذف ارزش شدت اثر")]
        SeverityValueDelete = 3084,
        #endregion
        #region TriggerStatus(3085-3089)

        [Display(GroupName = "TriggerStatus", Name = "GetAll", Description = "نمایش فهرست وضعیت محرک")]
        TriggerStatusGetAll = 3085,
        [Display(GroupName = "TriggerStatus", Name = "Get", Description = "نمایش جزئیات وضعیت محرک ")]
        TriggerStatusGet = 3086,
        [Display(GroupName = "TriggerStatus", Name = "Post", Description = "ثبت وضعیت محرک ")]
        TriggerStatusPost = 3087,
        [Display(GroupName = "TriggerStatus", Name = "Put", Description = "بروزرسانی وضعیت محرک ")]
        TriggerStatusPut = 3088,
        [Display(GroupName = "TriggerStatus", Name = "Delete", Description = "حذف وضعیت محرک")]
        TriggerStatusDelete = 3089,
        #endregion
        #region UseVulnerability(3090-3094)

        [Display(GroupName = "UseVulnerability", Name = "GetAll", Description = "نمایش فهرست سهولت استفاده از آسیب پذیری")]
        UseVulnerabilityGetAll = 3090,
        [Display(GroupName = "UseVulnerability", Name = "Get", Description = "نمایش جزئیات سهولت استفاده از آسیب پذیری ")]
        UseVulnerabilityGet = 3091,
        [Display(GroupName = "UseVulnerability", Name = "Post", Description = "ثبت سهولت استفاده از آسیب پذیری ")]
        UseVulnerabilityPost = 3092,
        [Display(GroupName = "UseVulnerability", Name = "Put", Description = "بروزرسانی سهولت استفاده از آسیب پذیری ")]
        UseVulnerabilityPut = 3093,
        [Display(GroupName = "UseVulnerability", Name = "Delete", Description = "حذف سهولت استفاده از آسیب پذیری")]
        UseVulnerabilityDelete = 3094,
        #endregion
        #endregion
    }
}
