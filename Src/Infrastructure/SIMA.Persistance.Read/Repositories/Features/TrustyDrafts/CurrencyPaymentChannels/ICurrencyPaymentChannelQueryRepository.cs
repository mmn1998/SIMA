using SIMA.Application.Query.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.CurrencyPaymentChannels;

public interface ICurrencyPaymentChannelQueryRepository : IQueryRepository
{
    Task<GetCurrencyPaymentChannelQueryResult> GetById(GetCurrencyPaymentChannelQuery request);
    Task<Result<IEnumerable<GetCurrencyPaymentChannelQueryResult>>> GetAll(GetAllCurrencyPaymentChannelsQuery request);
}