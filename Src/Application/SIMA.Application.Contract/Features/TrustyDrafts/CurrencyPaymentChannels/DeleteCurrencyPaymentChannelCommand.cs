using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;

public class DeleteCurrencyPaymentChannelCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}