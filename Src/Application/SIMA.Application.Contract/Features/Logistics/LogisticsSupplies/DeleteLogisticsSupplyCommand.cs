using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.LogisticsSupplies;

public class DeleteLogisticsSupplyCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
