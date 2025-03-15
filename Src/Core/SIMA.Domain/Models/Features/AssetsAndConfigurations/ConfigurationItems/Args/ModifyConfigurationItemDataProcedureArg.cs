namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class ModifyConfigurationItemDataProcedureArg
{
    public long Id { get; set; }
    public long DataProcedureId { get; set; }
    public long ConfigurationItemId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ModifiedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
}