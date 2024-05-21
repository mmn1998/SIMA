namespace SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Args;

public class ModifyMaximumAcceptableOutageArg
{
    public int DurationHourFrom { get; set; }
    public int? DurationHourTo { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
