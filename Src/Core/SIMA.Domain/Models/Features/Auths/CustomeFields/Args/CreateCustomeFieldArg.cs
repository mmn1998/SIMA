namespace SIMA.Domain.Models.Features.Auths.CustomeFields.Args;

public class CreateCustomeFieldArg
{
    public long Id { get; set; }
    public long MainAggregateId { get; set; }
    public long CustomeFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? EnglishKey { get; set; }
    public string? IsMandatory { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}