namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;

public class CreateConfigurationItemCustomFieldArg
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long ConfigurationItemTypeId { get; set; }
    public long CustomFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? IsMandatory { get; set; }
    public string? DisplayName { get; set; }
    public string? BoundingViewName { get; set; }
    public string? ValueBoundingFeild { get; set; }
    public string? TextBoundingFeild { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}