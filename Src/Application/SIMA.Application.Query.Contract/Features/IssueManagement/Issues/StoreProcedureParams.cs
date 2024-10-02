namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class StoreProcedureParams
{
    public long CurrentProgressId { get; set; }
    public long ProgressId { get; set; }
    public long ProgressStoredProcedureParamId { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public string? JsonFormat { get; set; }
    public string? BoundFormat { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
    public string? UiInputElementName { get; set; }
    public long? UiInputElementId { get; set; }
    public string? ApiNameForDataBounding { get; set; }
    public string? ApiMethodAction { get; set; }
    public string? StoredProcedureForDataBounding { get; set; }
    public string? TextBoundName { get; set; }
    public string? ValueBoundName { get; set; }
    public IEnumerable<BoundFormatDeserialized>? DeserializedBoundFormat { get; set; }
}
public class BoundFormatDeserialized
{
    public string? columnName { get; set; }
    public string? columnTitle { get; set; }
    public string? columnControlType { get; set; }
    public bool? boundByDataSet { get; set; }
    public bool? isInputParam { get; set; }
    public long? documentTypeId { get; set; }
    public string? textBoundName { get; set; }
    public string? valueBoundName { get; set; }
    public string? apiNameForDataBounding { get; set; }
    public string? storedProcedureForDataBounding { get; set; }
}
