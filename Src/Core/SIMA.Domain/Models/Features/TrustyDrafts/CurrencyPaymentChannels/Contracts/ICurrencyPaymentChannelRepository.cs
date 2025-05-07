using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;

public interface ICurrencyPaymentChannelRepository : IRepository<CurrencyPaymentChannel>
{
    Task<CurrencyPaymentChannel> GetById(CurrencyPaymentChannelId id);
}