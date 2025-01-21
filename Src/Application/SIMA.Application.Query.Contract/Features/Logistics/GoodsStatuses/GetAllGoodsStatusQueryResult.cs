namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsStatuses;

public class GetAllGoodsStatusQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? IsRequiredItConfirmation { get; set; }
    public string? IsFinal { get; set; }
    public string? ActiveStatus { get; set; }
}
