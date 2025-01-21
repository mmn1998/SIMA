using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsStatues;

public class DeleteGoodsStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
