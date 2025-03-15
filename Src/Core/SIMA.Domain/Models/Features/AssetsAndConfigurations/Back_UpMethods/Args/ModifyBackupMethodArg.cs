namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Args;

public class ModifyBackupMethodArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}