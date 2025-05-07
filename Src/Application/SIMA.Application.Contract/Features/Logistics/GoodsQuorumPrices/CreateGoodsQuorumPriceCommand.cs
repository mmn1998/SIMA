using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsQuorumPrices;

public class CreateGoodsQuorumPriceCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal MinPrice { get; set; }
    public string? IsRequiredCeoConfirmation { get; set; }
    public string? IsRequiredBoardConfirmation { get; set; }
    public string? IsRequiredSupplierWrittenInquiry { get; set; }
}