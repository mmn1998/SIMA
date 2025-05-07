namespace SIMA.Domain.Models.Features.Auths.PositionLevels.Args;

public class ModifyPositionLevelArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}