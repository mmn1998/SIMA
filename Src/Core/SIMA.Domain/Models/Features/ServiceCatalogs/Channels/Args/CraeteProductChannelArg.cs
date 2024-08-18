namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;

public class CreateProductChannelArg
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long ChannelId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
