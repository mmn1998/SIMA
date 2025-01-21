using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;

public class CreateCurrencyPaymentChannelCommand : ICommand<Result<long>>
{
    public long LocationId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}