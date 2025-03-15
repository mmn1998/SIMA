namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;

public class ModifyDataProcedureInputParamArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? DataType { get; set; }
    public long? ParentId { get; set; }
    public string? IsMandatory { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}