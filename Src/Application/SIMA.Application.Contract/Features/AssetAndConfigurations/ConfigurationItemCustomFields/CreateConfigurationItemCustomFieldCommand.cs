using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemCustomFields
{
    public class CreateConfigurationItemCustomFieldCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public long CustomFieldTypeId { get; set; }
        public long ConfigurationItemTypeId { get; set; }
        public string? IsMandatory { get; set; }
        public long? ParentId { get; set; }
        public List<CreateAssetcustomfieldoption>? AssetCustomFieldOption { get; set; }
        public string? BoundingViewName { get; set; }
        public string? ValueBoundingFeild { get; set; }
        public string? TextBoundingFeild { get; set; }
    }
}

