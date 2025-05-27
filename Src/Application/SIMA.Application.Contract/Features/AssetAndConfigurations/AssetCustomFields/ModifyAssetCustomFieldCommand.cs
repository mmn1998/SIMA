using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields
{
    public class ModifyAssetCustomFieldCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
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
