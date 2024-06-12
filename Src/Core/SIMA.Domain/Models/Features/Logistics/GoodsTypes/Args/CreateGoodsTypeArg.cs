namespace SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;

public class CreateGoodsTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsRequireItConfirmation { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}