using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemIssue : Entity
{
    private ConfigurationItemIssue() { }
    private ConfigurationItemIssue(CreateConfigurationItemIssueArg arg)
    {
        Id = new(arg.Id);
        IssueId = new(arg.IssueId);
        ConfigurationItemVersioningId = new(arg.ConfigurationItemVersionId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemIssue Create(CreateConfigurationItemIssueArg arg)
    {
        return new ConfigurationItemIssue(arg);
    }
    public ConfigurationItemIssueId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public ConfigurationItemVersioningId ConfigurationItemVersioningId { get; private set; }
    public virtual ConfigurationItemVersioning ConfigurationItemVersioning { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}

