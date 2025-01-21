using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Logistics.GoodsStatues;

public class ModifyGoodsStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsRequiredItConfirmation { get; set; }
    public string? IsFinal { get;  set; }
}