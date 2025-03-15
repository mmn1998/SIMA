namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Args;

public class ModifyDataProcedureTypeArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
