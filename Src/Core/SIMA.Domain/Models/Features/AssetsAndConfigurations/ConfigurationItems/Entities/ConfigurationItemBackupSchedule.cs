using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemBackupSchedule : Entity
{
    private ConfigurationItemBackupSchedule() { }
    private ConfigurationItemBackupSchedule(CreateConfigurationItemBackupScheduleArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        BackupMethodId = new(arg.BackupMethodId);
        DataCenterId = new(arg.DataCenterId);
        BackupConfigurationItemId = new(arg.BackupConfigurationItemId);
        if (arg.TimeMeasurementId.HasValue) TimeMeasurementId = new(arg.TimeMeasurementId.Value);
        Duration = arg.Duration;
        StartTime = arg.StartTime;
        LastTestDate = arg.LastTestDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemBackupSchedule Create(CreateConfigurationItemBackupScheduleArg arg)
    {
        return new ConfigurationItemBackupSchedule(arg);
    }
    public ConfigurationItemBackupScheduleId Id { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public ConfigurationItemId BackupConfigurationItemId { get; private set; }
    public virtual ConfigurationItem BackupConfigurationItem { get; private set; }
    public BackupMethodId BackupMethodId { get; private set; }
    public virtual BackupMethod BackupMethod { get; private set; }
    public DataCenterId DataCenterId { get; private set; }
    public virtual DataCenter DataCenter { get; private set; }
    public TimeMeasurementId? TimeMeasurementId { get; private set; }
    public virtual TimeMeasurement TimeMeasurement { get; private set; }
    public long ActiveStatusId { get; private set; }
    public float? Duration { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? LastTestDate { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
