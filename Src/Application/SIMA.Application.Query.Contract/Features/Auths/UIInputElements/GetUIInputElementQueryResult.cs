namespace SIMA.Application.Query.Contract.Features.Auths.UIInputElements;

public class GetUIInputElementQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
}
