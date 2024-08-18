namespace SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Args;

public class CreateCustomeFieldTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsList { get; set; }
    public string? IsMultiSelect { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}