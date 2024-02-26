namespace SIMA.Application.Query.Contract.Features.Auths.SysConfigs;

public class GetSysConfigQueryResult
{
    public long Id { get; set; }

    public long? ConfigurationId { get; set; }

    public string? KeyValue { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? ModifyBy { get; set; }
}
