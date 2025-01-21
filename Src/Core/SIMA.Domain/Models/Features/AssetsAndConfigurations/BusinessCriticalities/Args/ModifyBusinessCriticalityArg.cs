namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;

public class ModifyBusinessCriticalityArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Ordering { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
