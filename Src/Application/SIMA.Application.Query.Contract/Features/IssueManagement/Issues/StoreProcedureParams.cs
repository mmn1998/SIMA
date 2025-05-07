namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class StoreProcedureParams
{
    public long CurrentProgressId { get; set; }
    public long ProgressId { get; set; }
    public long ProgressStoredProcedureParamId { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public string? JsonFormat { get; set; }
    public string? ComboIsCascade { get; set; }
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
    public string? FixedValue { get; set; }
    public string? isCurrency { get; set; }
    public IEnumerable<NewBoundFormatDeseralieze>? MultiDeserializedBoundFormat { get; set; }
    public IEnumerable<BoundFormatDeserialized>? SingleDeserializedBoundFormat { get; set; }
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
    public string? getComboDataBy { get; set; }
    public string? isCurrency { get; set; }


}
public interface DeserializeFormat
{

}
public class NewBoundFormatDeseralieze : DeserializeFormat
{
    public string? name { get; set; }
    public string? comboIsCascade { get; set; }
    public string? displayName { get; set; }
    public string? jsonFormat { get; set; }
    public string? boundFormat { get; set; }
    public string? isMultiSelect { get; set; }
    public string? isSingleSelect { get; set; }
    public string? hasInputInEachRecord { get; set; }
    public string? uiInputElementName { get; set; }
    public long? uiInputElementId { get; set; }
    public string? getDataBy { get; set; }
    public string? apiNameForDataBounding { get; set; }
    public string? apiMethodAction { get; set; }
    public string? storedProcedureForDataBounding { get; set; }
    public string? textBoundName { get; set; }
    public string? valueBoundName { get; set; }
    public string? fixedValue { get; set; }
    public IEnumerable<BoundFormatDeserialized>? singleDeserializedBoundFormat { get; set; }
    public long? currentProgressId { get; set; }
    public long? progressId { get; set; }
    public long? progressStoredProcedureParamId { get; set; }
    public string? isCurrency { get; set; }
}
