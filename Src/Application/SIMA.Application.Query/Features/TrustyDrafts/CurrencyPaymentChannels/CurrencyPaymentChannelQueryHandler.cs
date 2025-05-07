using SIMA.Application.Query.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.CurrencyPaymentChannels;

namespace SIMA.Application.Query.Features.TrustyDrafts.CurrencyPaymentChannels;

public class CurrencyPaymentChannelQueryHandler : IQueryHandler<GetCurrencyPaymentChannelQuery, Result<GetCurrencyPaymentChannelQueryResult>>,
    IQueryHandler<GetAllCurrencyPaymentChannelsQuery, Result<IEnumerable<GetCurrencyPaymentChannelQueryResult>>>
{
    private readonly ICurrencyPaymentChannelQueryRepository _repository;

    public CurrencyPaymentChannelQueryHandler(ICurrencyPaymentChannelQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetCurrencyPaymentChannelQueryResult>> Handle(GetCurrencyPaymentChannelQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetCurrencyPaymentChannelQueryResult>>> Handle(GetAllCurrencyPaymentChannelsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}