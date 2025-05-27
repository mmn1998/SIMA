namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;

public class CreateDataProcedureInputParamCommand
{
    public string? DataType { get; set; }
    public long? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? IsMandatory { get; set; }
}
