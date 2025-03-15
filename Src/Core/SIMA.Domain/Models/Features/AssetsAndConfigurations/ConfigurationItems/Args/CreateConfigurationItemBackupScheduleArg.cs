namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemBackupScheduleArg
{
    public long Id { get; set; }
    public long ConfigurationItemId { get; set; }
    public long BackupConfigurationItemId { get; set; }
    public long DataCenterId { get; set; }
    public long BackupMethodId { get; set; }
    public long? TimeMeasurementId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastTestDate { get; set; }
    public float? Duration { get; set; }
    public TimeOnly? StartTime { get; set; }
    public long? CreatedBy { get; set; }
}