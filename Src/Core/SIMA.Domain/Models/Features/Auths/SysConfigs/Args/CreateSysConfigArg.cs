namespace SIMA.Domain.Models.Features.Auths.SysConfigs.Args;

public class CreateSysConfigArg
{
    public long Id { get; set; }
    public long? ConfigurationId { get; set; }

    public string? KeyValue { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
