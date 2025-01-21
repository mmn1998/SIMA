using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;

public class GetCurrencyPaymentChannelQuery : IQuery<Result<GetCurrencyPaymentChannelQueryResult>>
{
    public long Id { get; set; }
}