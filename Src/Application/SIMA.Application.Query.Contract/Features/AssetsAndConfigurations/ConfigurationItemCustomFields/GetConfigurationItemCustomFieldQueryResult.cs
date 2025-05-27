using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields
{
    public class GetConfigurationItemCustomFieldQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public long CustomeFieldTypeId { get; set; }
        public string? CustomeFieldTypeName { get; set; }
        public string? IsMandetory { get; set; }
        public long ParentId { get; set; }
        public string? BoundingViewName { get; set; }
        public string? TextBoundingFeild { get; set; }
        public string? ValueBoundingFeild { get; set; }
        public long ActiveStatusId { get; set; }
        public string? ActiveStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PersianCreatedAt => DateHelper.ToPersianDate(CreatedAt);
        public IEnumerable<GetConfigurationItemCustomFieldOption>? AssetCustomFieldOptionList { get; set; }
    }
}

public class GetConfigurationItemCustomFieldOption
{
    public string? OptionValue { get; set; }
    public string? OptionText { get; set; }
}

