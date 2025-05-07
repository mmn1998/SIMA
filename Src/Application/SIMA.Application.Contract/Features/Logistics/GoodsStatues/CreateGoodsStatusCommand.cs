using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsStatues;

public class CreateGoodsStatusCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsRequiredItConfirmation { get; set; }
    public string? IsFinal { get; set; }
}