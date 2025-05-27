namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class CreateConfigurationItemBackupScheduleInfo
{
    public long BackupConfigurationItemId { get; set; }
    public long DataCenterId { get; set; }
    public long BackupMethodId { get; set; }
    public long TimeMeasurementId { get; set; }
    public string? StartTime { get; set; }
    public string? LastTestDate { get; set; }
    public float? Duration { get; set; }
}
