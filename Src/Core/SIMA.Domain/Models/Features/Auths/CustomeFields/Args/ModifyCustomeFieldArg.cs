namespace SIMA.Domain.Models.Features.Auths.CustomeFields.Args;

public class ModifyCustomeFieldArg
{
    public long MainAggregateId { get; set; }
    public long CustomeFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? EnglishKey { get; set; }
    public string? IsMandatory { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
