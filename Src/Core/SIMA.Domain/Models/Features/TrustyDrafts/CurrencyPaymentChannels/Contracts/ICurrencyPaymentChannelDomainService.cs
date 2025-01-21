using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;

public interface ICurrencyPaymentChannelDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CurrencyPaymentChannelId? id = null);
}