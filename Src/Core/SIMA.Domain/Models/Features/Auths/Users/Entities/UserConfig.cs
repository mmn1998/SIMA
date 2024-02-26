using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class UserConfig
{
    private UserConfig() { }
    private UserConfig(CreateUserConfigArg arg)
    {
        Id = new UserConfigId(IdHelper.GenerateUniqueId());
        UserId = new UserId(arg.UserId);
        ConfigurationId = new ConfigurationAttributeId(arg.ConfigurationId);
        KeyValue = arg.KeyValue;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = DateTime.Now;
    }
    public static async Task<UserConfig> Create(IUserService userService, CreateUserConfigArg arg)
    {
        /// TODO change to domainservice
        //var validator = new UserConfigValidation(userService);
        //await validator.ValidateAndThrowAsync(arg);
        return new UserConfig(arg);
    }
    public UserConfigId Id { get; private set; }

    public UserId? UserId { get; private set; }

    public ConfigurationAttributeId? ConfigurationId { get; private set; }

    public string? KeyValue { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual ConfigurationAttribute? Configuration { get; private set; }

    public virtual User? User { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
