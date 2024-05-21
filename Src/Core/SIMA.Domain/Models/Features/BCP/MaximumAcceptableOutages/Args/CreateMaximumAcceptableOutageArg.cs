namespace SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Args;

public class CreateMaximumAcceptableOutageArg
{
    public int DurationHourFrom { get; set; }
    public int? DurationHourTo { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}