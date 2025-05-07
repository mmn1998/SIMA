using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields
{
    public class CreateAssetCustomFieldCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public long CustomFieldTypeId { get; set; }
        public long AssetTypeId { get; set; }
        public string? IsMandatory { get; set; }
        public long? ParentId { get; set; }
        public List<CreateAssetCustomFieldOption>? AssetCustomFieldOption { get; set; }
        public string? BoundingViewName { get; set; }
        public string? ValueBoundingFeild { get; set; }
        public string? TextBoundingFeild { get; set; }
    }
}
