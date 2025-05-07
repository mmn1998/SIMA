namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;

public class CreateBusinessCriticalityArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Ordering { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}