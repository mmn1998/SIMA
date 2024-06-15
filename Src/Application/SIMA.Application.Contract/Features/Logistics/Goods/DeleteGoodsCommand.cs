using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.Goods;

public class DeleteGoodsCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}