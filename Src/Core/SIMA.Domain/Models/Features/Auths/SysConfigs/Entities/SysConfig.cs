using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Args;
using SIMA.Domain.Models.Features.Auths.SysConfigs.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;

public class SysConfig : Entity
{
    private SysConfig() { }
    private SysConfig(CreateSysConfigArg arg)
    {
        Id = new SysConfigId(IdHelper.GenerateUniqueId());
        if (arg.ConfigurationId.HasValue) ConfigurationId = new ConfigurationAttributeId(arg.ConfigurationId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        KeyValue = arg.KeyValue;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public long ActiveStatusId { get; private set; }
    public SysConfigId Id { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public static async Task<SysConfig> Create(CreateSysConfigArg arg)
    {
        return new SysConfig(arg);
    }


    public ConfigurationAttributeId? ConfigurationId { get; private set; }
    public string? KeyValue { get; private set; }

    public virtual ConfigurationAttribute? Configuration { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
