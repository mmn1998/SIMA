namespace SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Args;

public class ModifyGoodsQuorumPriceArg
{
    public string? IsRequiredCeoConfirmation { get; set; }
    public string? IsRequiredBoardConfirmation { get; set; }
    public string? IsRequiredSupplierWrittenInquiry { get; set; }
    public float MinPrice { get; set; }
    public float MaxPrice { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
