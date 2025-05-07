using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.UIInputElements.Args;

public class CreateUIInputElementArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}