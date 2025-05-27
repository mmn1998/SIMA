using System.ComponentModel.DataAnnotations;

namespace SIMA.Persistance
{
    public enum PermissionForAddEnum : int
    {
        #region AssetAndConfiguration(3501-3700)
        #region AssetPhysicalStatus
        [Display(GroupName = "AssetPhysicalStatus", Name = "GetAll", Description = "نمایش فهرست وضعیت فیزیکی دارایی")]
        AssetPhysicalStatusGetAll = 3501,
        [Display(GroupName = "AssetPhysicalStatus", Name = "Get", Description = "نمایش جزئیات وضعیت فیزیکی دارایی")]
        AssetPhysicalStatusGet = 3502,
        [Display(GroupName = "AssetPhysicalStatus", Name = "Post", Description = "ثبت وضعیت فیزیکی دارایی")]
        AssetPhysicalStatusPost = 3503,
        [Display(GroupName = "AssetPhysicalStatus", Name = "Put", Description = "بروزرسانی وضعیت فیزیکی دارایی")]
        AssetPhysicalStatusPut = 3504,
        [Display(GroupName = "AssetPhysicalStatus", Name = "Delete", Description = "حذف وضعیت فیزیکی دارایی")]
        AssetPhysicalStatusDelete = 3505,
        #endregion
        #region Asset
        [Display(GroupName = "Asset", Name = "GetAll", Description = "نمایش فهرست دارایی")]
        AssetGetAll = 3506,
        [Display(GroupName = "Asset", Name = "Get", Description = "نمایش جزئیات دارایی")]
        AssetGet = 3507,
        [Display(GroupName = "Asset", Name = "Post", Description = "ثبت دارایی")]
        AssetPost = 3508,
        [Display(GroupName = "Asset", Name = "Put", Description = "بروزرسانی دارایی")]
        AssetPut = 3509,
        [Display(GroupName = "Asset", Name = "Delete", Description = "حذف دارایی")]
        AssetDelete = 3510,
        #endregion
        #region AssetTechnicalStatus
        [Display(GroupName = "AssetTechnicalStatus", Name = "GetAll", Description = "نمایش فهرست وضعیت تکنیکی دارایی")]
        AssetTechnicalStatusGetAll = 3511,
        [Display(GroupName = "AssetTechnicalStatus", Name = "Get", Description = "نمایش جزئیات وضعیت تکنیکی دارایی")]
        AssetTechnicalStatusGet = 3512,
        [Display(GroupName = "AssetTechnicalStatus", Name = "Post", Description = "ثبت وضعیت تکنیکی دارایی")]
        AssetTechnicalStatusPost = 3513,
        [Display(GroupName = "AssetTechnicalStatus", Name = "Put", Description = "بروزرسانی وضعیت تکنیکی دارایی")]
        AssetTechnicalStatusPut = 3514,
        [Display(GroupName = "AssetTechnicalStatus", Name = "Delete", Description = "حذف وضعیت تکنیکی دارایی")]
        AssetTechnicalStatusDelete = 3515,
        #endregion
        #region AssetType
        [Display(GroupName = "AssetType", Name = "GetAll", Description = "نمایش فهرست نوع دارایی")]
        AssetTypeGetAll = 3516,
        [Display(GroupName = "AssetType", Name = "Get", Description = "نمایش جزئیات نوع دارایی")]
        AssetTypeGet = 3517,
        [Display(GroupName = "AssetType", Name = "Post", Description = "ثبت نوع دارایی")]
        AssetTypePost = 3518,
        [Display(GroupName = "AssetType", Name = "Put", Description = "بروزرسانی نوع دارایی")]
        AssetTypePut = 3519,
        [Display(GroupName = "AssetType", Name = "Delete", Description = "حذف نوع دارایی")]
        AssetTypeDelete = 3520,
        #endregion
        #region BackupMethod
        [Display(GroupName = "BackupMethod", Name = "GetAll", Description = "نمایش فهرست روش پشتیبان گیری")]
        BackupMethodGetAll = 3521,
        [Display(GroupName = "BackupMethod", Name = "Get", Description = "نمایش جزئیات روش پشتیبان گیری")]
        BackupMethodGet = 3522,
        [Display(GroupName = "BackupMethod", Name = "Post", Description = "ثبت روش پشتیبان گیری")]
        BackupMethodPost = 3523,
        [Display(GroupName = "BackupMethod", Name = "Put", Description = "بروزرسانی روش پشتیبان گیری")]
        BackupMethodPut = 3524,
        [Display(GroupName = "BackupMethod", Name = "Delete", Description = "حذف روش پشتیبان گیری")]
        BackupMethodDelete = 3525,
        #endregion
        #region BusinessCriticality
        [Display(GroupName = "BusinessCriticality", Name = "GetAll", Description = "نمایش فهرست کسب و کار حیاتی")]
        BusinessCriticalityGetAll = 3526,
        [Display(GroupName = "BusinessCriticality", Name = "Get", Description = "نمایش جزئیات کسب و کار حیاتی")]
        BusinessCriticalityGet = 3527,
        [Display(GroupName = "BusinessCriticality", Name = "Post", Description = "ثبت کسب و کار حیاتی")]
        BusinessCriticalityPost = 3528,
        [Display(GroupName = "BusinessCriticality", Name = "Put", Description = "بروزرسانی کسب و کار حیاتی")]
        BusinessCriticalityPut = 3529,
        [Display(GroupName = "BusinessCriticality", Name = "Delete", Description = "حذف کسب و کار حیاتی")]
        BusinessCriticalityDelete = 3530,
        #endregion
        #region Category
        [Display(GroupName = "Category", Name = "GetAll", Description = "نمایش فهرست کسب و کار حیاتی")]
        CategoryGetAll = 3531,
        [Display(GroupName = "Category", Name = "Get", Description = "نمایش جزئیات کسب و کار حیاتی")]
        CategoryGet = 3532,
        [Display(GroupName = "Category", Name = "Post", Description = "ثبت کسب و کار حیاتی")]
        CategoryPost = 3533,
        [Display(GroupName = "Category", Name = "Put", Description = "بروزرسانی کسب و کار حیاتی")]
        CategoryPut = 3534,
        [Display(GroupName = "Category", Name = "Delete", Description = "حذف کسب و کار حیاتی")]
        CategoryDelete = 3535,
        #endregion
        #region ConfigurationItemRelationshipType
        [Display(GroupName = "ConfigurationItemRelationshipType", Name = "GetAll", Description = "نمایش فهرست نوع رابطه قلم پیکربندی")]
        ConfigurationItemRelationshipTypeGetAll = 3536,
        [Display(GroupName = "ConfigurationItemRelationshipType", Name = "Get", Description = "نمایش جزئیات نوع رابطه قلم پیکربندی")]
        ConfigurationItemRelationshipTypeGet = 3537,
        [Display(GroupName = "ConfigurationItemRelationshipType", Name = "Post", Description = "ثبت نوع رابطه قلم پیکربندی")]
        ConfigurationItemRelationshipTypePost = 3538,
        [Display(GroupName = "ConfigurationItemRelationshipType", Name = "Put", Description = "بروزرسانی نوع رابطه قلم پیکربندی")]
        ConfigurationItemRelationshipTypePut = 3539,
        [Display(GroupName = "ConfigurationItemRelationshipType", Name = "Delete", Description = "حذف نوع رابطه قلم پیکربندی")]
        ConfigurationItemRelationshipTypeDelete = 3540,
        #endregion
        #region ConfigurationItemStatus
        [Display(GroupName = "ConfigurationItemStatus", Name = "GetAll", Description = "نمایش فهرست نوع وضعیت قلم پیکربندی")]
        ConfigurationItemStatusGetAll = 3541,
        [Display(GroupName = "ConfigurationItemStatus", Name = "Get", Description = "نمایش جزئیات نوع وضعیت قلم پیکربندی")]
        ConfigurationItemStatusGet = 3542,
        [Display(GroupName = "ConfigurationItemStatus", Name = "Post", Description = "ثبت نوع وضعیت قلم پیکربندی")]
        ConfigurationItemStatusPost = 3543,
        [Display(GroupName = "ConfigurationItemStatus", Name = "Put", Description = "بروزرسانی نوع وضعیت قلم پیکربندی")]
        ConfigurationItemStatusPut = 3544,
        [Display(GroupName = "ConfigurationItemStatus", Name = "Delete", Description = "حذف نوع وضعیت قلم پیکربندی")]
        ConfigurationItemStatusDelete = 3545,
        #endregion
        #region ConfigurationItemType
        [Display(GroupName = "ConfigurationItemType", Name = "GetAll", Description = "نمایش فهرست نوع قلم پیکربندی")]
        ConfigurationItemTypeGetAll = 3546,
        [Display(GroupName = "ConfigurationItemType", Name = "Get", Description = "نمایش جزئیات نوع قلم پیکربندی")]
        ConfigurationItemTypeGet = 3547,
        [Display(GroupName = "ConfigurationItemType", Name = "Post", Description = "ثبت نوع قلم پیکربندی")]
        ConfigurationItemTypePost = 3548,
        [Display(GroupName = "ConfigurationItemType", Name = "Put", Description = "بروزرسانی نوع قلم پیکربندی")]
        ConfigurationItemTypePut = 3549,
        [Display(GroupName = "ConfigurationItemType", Name = "Delete", Description = "حذف نوع قلم پیکربندی")]
        ConfigurationItemTypeDelete = 3550,
        #endregion
        #region ConfigurationItem
        [Display(GroupName = "ConfigurationItem", Name = "GetAll", Description = "نمایش فهرست قلم پیکربندی")]
        ConfigurationItemGetAll = 3551,
        [Display(GroupName = "ConfigurationItem", Name = "Get", Description = "نمایش جزئیات قلم پیکربندی")]
        ConfigurationItemGet = 3552,
        [Display(GroupName = "ConfigurationItem", Name = "Post", Description = "ثبت قلم پیکربندی")]
        ConfigurationItemPost = 3553,
        [Display(GroupName = "ConfigurationItem", Name = "Put", Description = "بروزرسانی قلم پیکربندی")]
        ConfigurationItemPut = 3554,
        [Display(GroupName = "ConfigurationItem", Name = "Delete", Description = "حذف قلم پیکربندی")]
        ConfigurationItemDelete = 3555,
        #endregion
        #region DataCenter
        [Display(GroupName = "DataCenter", Name = "GetAll", Description = "نمایش فهرست مرکز داده")]
        DataCenterGetAll = 3556,
        [Display(GroupName = "DataCenter", Name = "Get", Description = "نمایش جزئیات مرکز داده")]
        DataCenterGet = 3557,
        [Display(GroupName = "DataCenter", Name = "Post", Description = "ثبت مرکز داده")]
        DataCenterPost = 3558,
        [Display(GroupName = "DataCenter", Name = "Put", Description = "بروزرسانی مرکز داده")]
        DataCenterPut = 3559,
        [Display(GroupName = "DataCenter", Name = "Delete", Description = "حذف مرکز داده")]
        DataCenterDelete = 3560,
        #endregion
        #region DataProcedure
        [Display(GroupName = "DataProcedure", Name = "GetAll", Description = "نمایش فهرست رویه داده")]
        DataProcedureGetAll = 3561,
        [Display(GroupName = "DataProcedure", Name = "Get", Description = "نمایش جزئیات رویه داده")]
        DataProcedureGet = 3562,
        [Display(GroupName = "DataProcedure", Name = "Post", Description = "ثبت رویه داده")]
        DataProcedurePost = 3563,
        [Display(GroupName = "DataProcedure", Name = "Put", Description = "بروزرسانی رویه داده")]
        DataProcedurePut = 3564,
        [Display(GroupName = "DataProcedure", Name = "Delete", Description = "حذف رویه داده")]
        DataProcedureDelete = 3565,
        #endregion
        #region DataProcedureType
        [Display(GroupName = "DataProcedureType", Name = "GetAll", Description = "نمایش فهرست نوع رویه داده")]
        DataProcedureTypeGetAll = 3566,
        [Display(GroupName = "DataProcedureType", Name = "Get", Description = "نمایش جزئیات نوع رویه داده")]
        DataProcedureTypeGet = 3567,
        [Display(GroupName = "DataProcedureType", Name = "Post", Description = "ثبت نوع رویه داده")]
        DataProcedureTypePost = 3568,
        [Display(GroupName = "DataProcedureType", Name = "Put", Description = "بروزرسانی نوع رویه داده")]
        DataProcedureTypePut = 3569,
        [Display(GroupName = "DataProcedureType", Name = "Delete", Description = "حذف نوع رویه داده")]
        DataProcedureTypeDelete = 3570,
        #endregion
        #region LicenseType
        [Display(GroupName = "LicenseType", Name = "GetAll", Description = "نمایش فهرست نوع گواهینامه")]
        LicenseTypeGetAll = 3571,
        [Display(GroupName = "LicenseType", Name = "Get", Description = "نمایش جزئیات نوع گواهینامه")]
        LicenseTypeGet = 3572,
        [Display(GroupName = "LicenseType", Name = "Post", Description = "ثبت نوع گواهینامه")]
        LicenseTypePost = 3573,
        [Display(GroupName = "LicenseType", Name = "Put", Description = "بروزرسانی نوع گواهینامه")]
        LicenseTypePut = 3574,
        [Display(GroupName = "LicenseType", Name = "Delete", Description = "حذف نوع گواهینامه")]
        LicenseTypeDelete = 3575,
        #endregion
        #region OperationalStatus
        [Display(GroupName = "OperationalStatus", Name = "GetAll", Description = "نمایش فهرست وضعیت عملیاتی")]
        OperationalStatusGetAll = 3576,
        [Display(GroupName = "OperationalStatus", Name = "Get", Description = "نمایش جزئیات وضعیت عملیاتی")]
        OperationalStatusGet = 3577,
        [Display(GroupName = "OperationalStatus", Name = "Post", Description = "ثبت وضعیت عملیاتی")]
        OperationalStatusPost = 3578,
        [Display(GroupName = "OperationalStatus", Name = "Put", Description = "بروزرسانی وضعیت عملیاتی")]
        OperationalStatusPut = 3579,
        [Display(GroupName = "OperationalStatus", Name = "Delete", Description = "حذف وضعیت عملیاتی")]
        OperationalStatusDelete = 3580,
        #endregion

        #region API
        [Display(GroupName = "API", Name = "GetAll", Description = "نمایش فهرست رابط برنامه نویسی")]
        APIGetAll = 3581,
        [Display(GroupName = "API", Name = "Get", Description = "نمایش جزئیات رابط برنامه نویسی")]
        APIGet = 3582,
        [Display(GroupName = "API", Name = "Post", Description = "ثبت رابط برنامه نویسی")]
        APIPost = 3583,
        [Display(GroupName = "API", Name = "Put", Description = "بروزرسانی رابط برنامه نویسی")]
        APIPut = 3584,
        [Display(GroupName = "API", Name = "Delete", Description = "حذف رابط برنامه نویسی")]
        APIDelete = 3585,
        #endregion

        #region AssetCustomField
        [Display(GroupName = "AssetCustomField", Name = "GetAll", Description = "نمایش فهرست فیلدهای سفارشی دارایی")]
        AssetCustomFieldGetAll = 3586,
        [Display(GroupName = "AssetCustomField", Name = "Get", Description = "نمایش جزئیات فیلدهای سفارشی دارایی")]
        AssetCustomFieldGet = 3587,
        [Display(GroupName = "AssetCustomField", Name = "Post", Description = "ثبت فیلدهای سفارشی دارایی")]
        AssetCustomFieldPost = 3588,
        [Display(GroupName = "AssetCustomField", Name = "Put", Description = "بروزرسانی فیلدهای سفارشی دارایی")]
        AssetCustomFieldPut = 3589,
        [Display(GroupName = "AssetCustomField", Name = "Delete", Description = "حذف فیلدهای سفارشی دارایی")]
        AssetCustomFieldDelete = 3590,
        #endregion

        #region ConfigurationItemCustomField
        [Display(GroupName = "ConfigurationItemCustomField", Name = "GetAll", Description = "نمایش فهرست فیلدهای سفارشی قلم پیکربندی")]
        ConfigurationItemCustomFieldGetAll = 3591,
        [Display(GroupName = "ConfigurationItemCustomField", Name = "Get", Description = "نمایش جزئیات فیلدهای سفارشی قلم پیکربندی")]
        ConfigurationItemCustomFieldGet = 3592,
        [Display(GroupName = "ConfigurationItemCustomField", Name = "Post", Description = "ثبت فیلدهای سفارشی قلم پیکربندی")]
        ConfigurationItemCustomFieldPost = 3593,
        [Display(GroupName = "ConfigurationItemCustomField", Name = "Put", Description = "بروزرسانی فیلدهای سفارشی قلم پیکربندی")]
        ConfigurationItemCustomFieldPut = 3594,
        [Display(GroupName = "ConfigurationItemCustomField", Name = "Delete", Description = "حذف فیلدهای سفارشی قلم پیکربندی")]
        ConfigurationItemCustomFieldDelete = 3595,
        #endregion

        #region CustomeFieldType
        [Display(GroupName = "CustomeFieldType", Name = "GetAll", Description = "نمایش فهرست نوع فیلد های سفارشی")]
        CustomeFieldTypeGetAll = 3596,
        [Display(GroupName = "CustomeFieldType", Name = "Get", Description = "نمایش جزئیات نوع فیلد های سفارشی")]
        CustomeFieldTypeGet = 3597,
        [Display(GroupName = "CustomeFieldType", Name = "Post", Description = "ثبت نوع فیلد های سفارشی")]
        CustomeFieldTypePost = 3598,
        [Display(GroupName = "CustomeFieldType", Name = "Put", Description = "بروزرسانی نوع فیلد های سفارشی")]
        CustomeFieldTypePut = 3599,
        [Display(GroupName = "CustomeFieldType", Name = "Delete", Description = "حذف نوع فیلد های سفارشی")]
        CustomeFieldTypeDelete = 3600,
        #endregion

        #region LicenseStatus
        [Display(GroupName = "LicenseStatus", Name = "GetAll", Description = "نمایش فهرست وضعیت لایسنس")]
        LicenseStatusGetAll = 3601,
        [Display(GroupName = "LicenseStatus", Name = "Get", Description = "نمایش جزئیات وضعیت لایسنس")]
        LicenseStatusGet = 3602,
        [Display(GroupName = "LicenseStatus", Name = "Post", Description = "ثبت وضعیت لایسنس")]
        LicenseStatusPost = 3603,
        [Display(GroupName = "LicenseStatus", Name = "Put", Description = "بروزرسانی وضعیت لایسنس")]
        LicenseStatusPut = 3604,
        [Display(GroupName = "LicenseStatus", Name = "Delete", Description = "حذف وضعیت لایسنس")]
        LicenseStatusDelete = 3605,
        #endregion

        #endregion
    }
}
