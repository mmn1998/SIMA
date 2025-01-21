namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;

public class CreateChannelAccessPointArg
{
    public long Id { get; set; }
    public long ChannelId { get; set; }
    public string? IpAddressTo { get; set; }
    public string? PortTo { get; set; }
    public string? IpAddressFrom { get; set; }
    public string? PortFrom { get; set; }
    public DateTime? ActivationDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}