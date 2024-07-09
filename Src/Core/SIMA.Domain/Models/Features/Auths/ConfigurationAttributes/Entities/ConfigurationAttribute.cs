using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Args;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;

public class ConfigurationAttribute : Entity
{
    private ConfigurationAttribute()
    { }
    private ConfigurationAttribute(CreateConfigurationAttributeArg arg)
    {
        Id = new ConfigurationAttributeId(IdHelper.GenerateUniqueId());
        EnglishKey = arg.EnglishKey;
        Name = arg.Name;
        IsUserConfige = arg.IsUserConfige;
        ActiveStatusId = arg.ActiveStatusId;
        DataType = arg.DataType;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationAttribute> Create(CreateConfigurationAttributeArg arg, IConfigurationAttributeService service)
    {
        /// TODO change to domainservice
        //var validator = new ConfigurationAttributeValidator(service);
        //await validator.ValidateAndThrowAsync(arg);
        return new ConfigurationAttribute(arg);
    }


    public ConfigurationAttributeId Id { get; private set; }

    public string? EnglishKey { get; private set; }

    public string? Name { get; private set; }

    public string? DataType { get; private set; }

    public string? IsUserConfige { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual ICollection<ConfigurationAttributeValue> ConfigurationAttributeValues { get; private set; } = new List<ConfigurationAttributeValue>();
    private List<SysConfig> _sysConfigs = new();
    public ICollection<SysConfig> SysConfigs => _sysConfigs;
    private List<UserConfig> _userConfigs = new();

    public ICollection<UserConfig> UserConfigs => _userConfigs;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
