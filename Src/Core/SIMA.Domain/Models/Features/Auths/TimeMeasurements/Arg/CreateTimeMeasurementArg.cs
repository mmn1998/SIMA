using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;

namespace SIMA.Domain.Models.Features.Auths.TimeMeasurements.Arg;

public class CreateTimeMeasurementArg
{
    public long Id { get; set; }
    public long UnitBasement { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}public class ModifyTimeMeasurementArg
{
    public long Id { get; set; }
    public long UnitBasement { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
