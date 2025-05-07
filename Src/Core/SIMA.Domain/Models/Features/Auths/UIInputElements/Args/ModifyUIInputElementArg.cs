namespace SIMA.Domain.Models.Features.Auths.UIInputElements.Args;

public class ModifyUIInputElementArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
