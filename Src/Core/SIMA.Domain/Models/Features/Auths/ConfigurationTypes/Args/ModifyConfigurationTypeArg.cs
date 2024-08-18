namespace SIMA.Domain.Models.Features.Auths.ConfigurationTypes.Args;

public class ModifyConfigurationTypeArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsList { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}