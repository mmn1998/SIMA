namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;

public class CreateChannelAccessPointArg
{
    public long Id { get; set; }
    public long ChannelId { get; set; }
    public string? IpAddress { get; set; }
    public string? Port { get; set; }
    public DateTime? ActivationDate { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }

}