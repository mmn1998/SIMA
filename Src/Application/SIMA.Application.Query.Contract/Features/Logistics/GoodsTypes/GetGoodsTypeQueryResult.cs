namespace SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;

public class GetGoodsTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsRequiredItConfirmation { get; set; }
    public string? ActiveStatus { get; set; }
}