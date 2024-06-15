using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsQuorumPrices;

public class DeleteGoodsQuorumPriceCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}