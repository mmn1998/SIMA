namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;

public class ModifyConfigurationItemCustomFieldArg
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long ConfigurationItemTypeId { get; set; }
    public long CustomeFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? IsMandetory { get; set; }
    public string? DisplayName { get; set; }
    public string? BoundingViewName { get; set; }
    public string? ValueBoundingFeild { get; set; }
    public string? TextBoundingFeild { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}