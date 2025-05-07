namespace SIMA.Domain.Models.Features.Auths.PositionTypes.Args;

public class ModifyPositionTypeArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}